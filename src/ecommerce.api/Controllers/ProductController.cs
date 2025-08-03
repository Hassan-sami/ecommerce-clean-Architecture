using ecommerce.api.Common;
using ecommerce.Application.Cqrs.Products.Commands.CreateProduct;
using ecommerce.Application.Cqrs.Products.Commands.DeleteProduct;
using ecommerce.Application.Cqrs.Products.Commands.UpdateProduct;
using ecommerce.Application.Cqrs.Products.queries;
using ecommerce.Application.Cqrs.Products.queries.Proudcts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]PorductGetByIdQuery query)
        {
            var product = await mediator.Send(query);
            return NewResult(product);

        }
        [HttpPost("[action]/")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody]CreateProductCommand command)
        {
            var product = await mediator.Send(command);
            return NewResult(product);
        }
        [HttpGet("[action]/")]
        public async Task<IActionResult> GetProducts([FromRoute] GetProductsQuery query)
        {
            var product = await mediator.Send(query);
            return NewResult(product);
        }
        [HttpGet("paginatedProducts/")]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsPaginatedQuery query)
        {
            var product = await mediator.Send(query);
            return NewResult(product);
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteById([FromRoute]DeleteProductByIdCommand byIdCommand)
        {
            var result =await mediator.Send(byIdCommand);
            return NewResult(result);
        }
        [HttpPut("[action]/")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            var result =await mediator.Send(command);
            return NewResult(result);
        }
    }
}
