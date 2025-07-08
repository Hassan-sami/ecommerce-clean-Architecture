using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Application.Cqrs.Products.queries
{
    public class ProductGetByIdCommandhandler : IRequestHandler<PorductGetByIdQuery, Domain.Enitities.Product>
    {
        private readonly IProductService productService;

        public ProductGetByIdCommandhandler(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<Product> Handle(PorductGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await productService.GetProductByIdAsync(request.id);
                
        }
    }
}
