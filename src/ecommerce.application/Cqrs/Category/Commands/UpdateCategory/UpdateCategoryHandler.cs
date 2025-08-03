using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Category.Commands.UpdateCategory;

public class UpdateCategoryHandler(IStringLocalizer<Resource> stringLocalizer,
    ICateogryService cateogryService, IMapper mapper) : ResponseHandler(stringLocalizer), IRequestHandler<UpdateCategoryCommand, Response<string>>
{
    public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Domain.Enitities.Category>(request);
        var cat = await cateogryService.GetCategoryByIdAsync(request.Id);
        if (cat == null)
            return BadRequest<string>();
        cat.Name= category.Name;
        cat.Description = category.Description;
        cat.ImageName = category.ImageName;
        await cateogryService.UpdateCategoryAsync(category);
        return Success("Category updated");
    }
}