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
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using ecommerce.Application.Resources;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Products.Commands.CreateProduct
{
    class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductResponse>>
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<Resource> _localizer;

        public CreateProductCommandHandler(IProductService productService, IMapper mapper,
                            IStringLocalizer<Resource> localizer)
        {
            this.productService = productService;
            this.mapper = mapper;
            _localizer = localizer;
        }
        public async Task<Response<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product  = mapper.Map<Product>(request);
            await productService.Create(product);
            var response = mapper.Map<ProductResponse>(product);
            return new ResponseHandler(_localizer).Created(response);
        }
    }

}
