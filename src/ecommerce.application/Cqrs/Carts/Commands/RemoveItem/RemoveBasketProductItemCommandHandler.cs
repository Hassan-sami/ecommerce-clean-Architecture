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

namespace ecommerce.Application.Cqrs.Carts.Commands.RemoveItem;

public class RemoveBasketProductItemCommandHandler(IStringLocalizer<Resource> stringLocalizer, 
                                                IHttpContextAccessor contextAccessor,
                                                ICartService cartService) : ResponseHandler(stringLocalizer),IRequestHandler<RemoveBasketProductItemCommand,Response<Guid>>
{
    public async Task<Response<Guid>> Handle(RemoveBasketProductItemCommand request, CancellationToken cancellationToken)
    {
        var user = contextAccessor.HttpContext.User;
        if (user is null)
            return BadRequest<Guid>();
        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if(userId is null)
            return BadRequest<Guid>();
        var includes = new List<Expression<Func<Cart, object>>>()
        {
            (c => c.Items)
        };
        var spec = new CartWithItemsSpec(c => c.UserId == new Guid(userId),includes);
        var cart = await cartService.GetCartWithSpecAsync(spec);
        if(cart == null)
            return NotFound<Guid>();
        
        var item = cart.Items?.FirstOrDefault(i => i.ProductId == request.ProductId);
        if(item == null)
            return BadRequest<Guid>();
        cart.Items?.Remove(item);
        await cartService.UpdateAsync(cart);
        return Success(cart.Id);
    }
}