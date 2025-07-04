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
        public string UserName { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
