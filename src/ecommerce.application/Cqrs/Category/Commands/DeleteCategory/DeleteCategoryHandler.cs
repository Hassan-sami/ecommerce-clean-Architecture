using ecommerce.Application.Base;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Category.Commands.DeleteCategory;

public class DeleteCategoryHandler(IStringLocalizer<Resource> stringLocalizer, ICateogryService cateogryService) : ResponseHandler(stringLocalizer), IRequestHandler<DeleteCategoryCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await cateogryService.DeleteCategoryAsync(request.Id);
        if (category == null)
            return BadRequest<string>();
        return Success("Category deleted successfully");
    }
}