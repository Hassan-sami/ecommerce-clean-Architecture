using ecommerce.Application.Interfaces;
using MediatR;
using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using ecommerce.Application.Resources;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Products.queries
{
    public class ProductGetByIdCommandhandler : ResponseHandler,IRequestHandler<PorductGetByIdQuery, Response<ProductResponse>>
    {
        private readonly IProductService productService;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public ProductGetByIdCommandhandler(IProductService productService, IStringLocalizer<Resource> localizer,IMapper mapper) :base(localizer)
        {
            this.productService = productService;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Response<ProductResponse>> Handle(PorductGetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productService.GetProdctByIdWithCategoryAsync(request.id);
            if (product == null)
                 return NotFound<ProductResponse>(_localizer[LocalizationConstants.NotFound]);      
            var response = _mapper.Map<ProductResponse>(product);
            return Success(response);
        }
    }
}
