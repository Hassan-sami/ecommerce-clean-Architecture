using ecommerce.Domain.common;
using ecommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Domain.Enitities
{
    public class Order : BaseEntity
    {
        private Order(Guid userId)
        {
            this.UserId = userId;
        }
        public Guid UserId { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get => Items.Sum(o => o.Quantity * o.UnitPrice); }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public static Order Create(Cart cart)
        {
            Order order = new Order(cart.UserId);
            foreach(var item in cart?.Items)
            {
                order.AddOrderItem(item.ProductId, item.Quantity, item.Product.UnitPrice);
            }

            order.Status = OrderStatus.Finished;
            return order;
        }

        private void AddOrderItem(Guid productId, int quantity, decimal price)
        {
            Items.Add(OrderItem.Create(Id, productId, quantity, price));
        }
    }
}
