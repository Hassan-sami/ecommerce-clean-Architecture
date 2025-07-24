using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Products.queries.Proudcts;

public class GetProductsHandler : ResponseHandler,IRequestHandler<GetProductsQuery, Response<IReadOnlyList<ProductResponse>>>,
                                   IRequestHandler<GetProductsPaginatedQuery, Response<IReadOnlyList<ProductResponse>>>
{
    private readonly IProductService _productService;
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly IMapper _mapper;

    public GetProductsHandler(IProductService productService, 
                                IStringLocalizer<Resource> localizer,
                                 IMapper mapper) : base(localizer)
    {
        _productService = productService;
        _localizer = localizer;
        _mapper = mapper;
    }
    
    public async Task<Response<IReadOnlyList<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllProducts();
        var response = (IReadOnlyList<ProductResponse>)products.Select(p => _mapper.Map<ProductResponse>(p)).ToList();
        return Success<IReadOnlyList<ProductResponse>>(response);
    }

    public async Task<Response<IReadOnlyList<ProductResponse>>> Handle(GetProductsPaginatedQuery request, CancellationToken cancellationToken)
    {
          var products = await _productService.GetProductsPaginatedAsync((request.page - 1) * request.size,request.size);
          if (products == null)
              return NotFound<IReadOnlyList<ProductResponse>>(_localizer[LocalizationConstants.NotFound]);
          var response = (IReadOnlyList<ProductResponse>)products.Select(p => _mapper.Map<ProductResponse>(p)).ToList();
          return Success<IReadOnlyList<ProductResponse>>(response);
    }
}