using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Category.Responses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using ecommerce.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Category.Commands.CreateCategory;

public class CreateCategoryHandler(IStringLocalizer<Resource> stringLocalizer, 
    ICateogryService cateogryService, IMapper mapper) : ResponseHandler(stringLocalizer), IRequestHandler<CreateCategoryCommand ,Response<CategoryResponse>>
{
    
    public  async Task<Response<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Domain.Enitities.Category>(request);
        category.Id = Guid.NewGuid();
       var res =  await cateogryService.AddCategoryAsync(category);
       if (res == null)
           return BadRequest<CategoryResponse>();
       var response = new CategoryResponse
       {
            Id = category.Id,
            Name = category.Name,
            
       };
       return Created(response);
    }
}