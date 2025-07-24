using AutoMapper;
using ecommerce.Application.Cqrs.Carts.Commands.CreateCart;
using ecommerce.Application.Cqrs.Carts.Reponses;
using ecommerce.Domain.Enitities;

namespace ecommerce.Application.Mappers.CartMappers;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<CreateCartCommand, Cart>();
        CreateMap<Cart, CartResponse>();
    }
}