using System.Linq.Expressions;
using System.Security.Claims;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Carts.Reponses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Carts.Commands.CheckOut;

public class CheckOutHandler(IStringLocalizer<Resource> stringLocalizer,
                        IHttpContextAccessor contextAccessor, ICartService cartService, ICheckOutService checkOutService) : ResponseHandler(stringLocalizer), IRequestHandler<CheckOutCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(CheckOutCommand request, CancellationToken cancellationToken)
    {
        var user = contextAccessor.HttpContext.User;
        if (user is null)
            return BadRequest<Guid>();
        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.AuthenticationMethod)?.Value;
        if (string.IsNullOrEmpty(userId))
            return BadRequest<Guid>();

        var personalInfo = new PersonalInfo()
        {
            City = request.City,
            Name = request.Name,
            Street = request.Street,
        };
        var includes = new List<Expression<Func<Cart, object>>>()
        {
            (c => c.Items)
        };
        var spec = new CartWithItemsSpec(c => c.UserId == new Guid(userId),includes);
        var cart  = await cartService.GetCartWithSpecAsync(spec);
        if (cart is null)
            return NotFound<Guid>();
        if (!cart.Items.Any())
            return BadRequest<Guid>();
        Order result = await checkOutService.CheckOut(cart, personalInfo);
        return Success(result.Id);
    }
}