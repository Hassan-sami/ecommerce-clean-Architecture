using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Domain.Enitities
{
    public class OrderItem
    {
        private OrderItem(
            Guid orderId,
            Guid productId,
            int quantity,
            decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = price;
            
        }
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; private set; }
        public Order Order { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public static OrderItem Create(Guid orderId, Guid productId, int quantity, decimal price)
        {
            OrderItem orderItem = new OrderItem(
                orderId,
                productId,
                quantity,
                price);

            return orderItem;
        }
        
    }
}
