using ecommerce.Application.Cqrs.Carts.Reponses;
using ecommerce.Domain.Enitities;

namespace ecommerce.Application.Interfaces;

public interface ICheckOutService
{
    ValueTask<Order> CheckOut(Cart cart,PersonalInfo pernsonalInfo);
}