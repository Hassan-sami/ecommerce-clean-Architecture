using ecommerce.Application.Cqrs.Products.queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]PorductGetByIdQuery query)
        {
            
            var product = await mediator.Send(query);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
