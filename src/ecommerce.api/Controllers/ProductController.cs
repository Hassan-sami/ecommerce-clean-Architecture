using System.Net;
using ecommerce.api.Common;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.Commands.CreateProduct;
using ecommerce.Application.Cqrs.Products.queries;
using ecommerce.Application.Cqrs.Products.queries.Proudcts;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using MediatR;
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
    }
}
