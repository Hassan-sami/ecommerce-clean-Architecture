using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Category.Responses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Category.Queries.GetById;

public class GetHander(IStringLocalizer<Resource> stringLocalizer, ICateogryService cateogryService, IMapper mapper) : ResponseHandler(stringLocalizer),IRequestHandler<GetCategoryByIdQuery,Response<CategoryResponse>>
    ,IRequestHandler<GetCategoriesPaginatedQuery,Response<IReadOnlyList<CategoryResponse>>>
{
    public async Task<Response<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await cateogryService.GetCategoryByIdAsync(request.id);
        if (category is null)
            return BadRequest<CategoryResponse>();
        var response = mapper.Map<CategoryResponse>(category);
        return Success<CategoryResponse>(response);
    }

    public async Task<Response<IReadOnlyList<CategoryResponse>>> Handle(GetCategoriesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var products = await cateogryService.GetCategoriesPaginatedAsync((request.page - 1) * request.size,request.size);
        if (products == null)
            return NotFound<IReadOnlyList<CategoryResponse>>(stringLocalizer[LocalizationConstants.NotFound]);
        var response = (IReadOnlyList<CategoryResponse>)products.Select(p => mapper.Map<CategoryResponse>(p)).ToList();
        return Success<IReadOnlyList<CategoryResponse>>(response);
    }
}