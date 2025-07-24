using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Authentication.commands.CreateUser;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Carts.Commands.CreateCart;

public class CreateCartHandler(IStringLocalizer<Resource> stringLocalizer, 
    ICartService cartService, IMapper mapper) : ResponseHandler(stringLocalizer), IRequestHandler<CreateCartCommand ,Response<string>>
{
    

    public async Task<Response<string>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = mapper.Map<Cart>(request);
        await cartService.CreateCart(cart);
        return Success("Added cart");
    }
}