using ecommerce.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Domain.Enitities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public double Star { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }        
        public Category Category { get; set; }
    }
}
