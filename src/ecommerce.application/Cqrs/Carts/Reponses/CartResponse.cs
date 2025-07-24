using ecommerce.Domain.Enitities;

namespace ecommerce.Application.Cqrs.Carts.Reponses;

public class CartResponse
{
    public Guid UserId { get; set; }
    public List<CartItem> Items { get; set; }
}