using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;
using ecommerce.Domain.Enitities;
using ecommerce.Application.Interfaces;
using AutoMapper;

namespace ecommerce.Application.Cqrs.Products.Commands.CreateProduct
{
    class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Domain.Enitities.Product>
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }
        public async Task<Domain.Enitities.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product  = mapper.Map<Product>(request);
            return await productService.Create(product);
        }
    }

}
