using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using ecommerce.Domain.Enitities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Products.Commands.UpdateProduct;

public class UpdateProductHandler(IStringLocalizer<Resource> stringLocalizer,
                                  IMapper mapper, IProductService productService) : 
                    ResponseHandler(stringLocalizer), IRequestHandler<UpdateProductCommand,Response<ProductResponse>>
{
    public async Task<Response<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productMapping = mapper.Map<Product>(request);
        if (productMapping is null)
            return BadRequest<ProductResponse>();
        var product = await productService.Update(productMapping);
        var response = mapper.Map<ProductResponse>(product);
        return Success(response);
    }
}