using System.Linq.Expressions;
using System.Security.Claims;
using ecommerce.Application.Base;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Carts.Commands.AddItem;

public class AddItemCommandHandler(IStringLocalizer<Resource> stringLocalizer, IHttpContextAccessor contextAccessor,
    IProductService productService, ICartService cartService) : ResponseHandler(stringLocalizer), IRequestHandler<AddItemCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var user = contextAccessor.HttpContext.User;
        if (user == null)
            return BadRequest<Guid>();
        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return BadRequest<Guid>();
        
        var product = await productService.GetProductByIdAsync(request.ProductId);
        if (product is null)
            return BadRequest<Guid>();
        var includes = new List<Expression<Func<Cart, object>>>()
        {
            (c => c.Items)
        };
        var spec = new CartWithItemsSpec(c => c.UserId == new Guid(userId),includes);
        var basket  = await cartService.GetCartWithSpecAsync(spec);
        if (basket is null)
        {
            var cart = new Cart(){UserId = new Guid(userId) };
            cart.AddItem(request.ProductId,  request.Quantity);
            basket = await cartService.CreateCart(cart);
        }
        else
        {
            basket.AddItem(request.ProductId, request.Quantity);
            await cartService.UpdateAsync(basket);
        }
        return Success(basket.Id);
    }
}