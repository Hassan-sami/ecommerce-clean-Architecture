using ecommerce.api.Common;
using ecommerce.Application.Cqrs.Category.Commands.CreateCategory;
using ecommerce.Application.Cqrs.Category.Commands.DeleteCategory;
using ecommerce.Application.Cqrs.Category.Commands.UpdateCategory;
using ecommerce.Application.Cqrs.Category.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;
[Route("[controller]")]
[ApiController]
public class CategoryController : BaseController
{

    public CategoryController(IMediator mediator) : base(mediator)
    {
    }

    
    [HttpPost("paginated")]
    public async Task<IActionResult> GetPaginatedCategories([FromBody] GetCategoriesPaginatedQuery request)
    {
        var result = await mediator.Send(request);
        return NewResult(result);
    }

    
    [HttpGet("[action]/{Id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute]GetCategoryByIdQuery query)
    {
        var response = await mediator.Send(query);
        return NewResult(response);
    }
    
    [HttpPost("[action]")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand request)
    {
        var result = await mediator.Send(request);
        return NewResult(result);
    }
    
    [HttpPut("[action]/{Id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateCategory([FromBody,FromRoute] UpdateCategoryCommand request)
    {
        var response = await mediator.Send(request);
        return NewResult(response);
    }
    
    [HttpDelete("[action]/{Id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteCategory([FromRoute]DeleteCategoryCommand command)
    {
        var response = await mediator.Send(command);
        return NewResult(response);;
    }
}
