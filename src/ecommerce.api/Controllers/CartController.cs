using ecommerce.api.Common;
using ecommerce.Application.Cqrs.Carts.Commands.AddItem;
using ecommerce.Application.Cqrs.Carts.Commands.CheckOut;
using ecommerce.Application.Cqrs.Carts.Commands.CreateCart;
using ecommerce.Application.Cqrs.Carts.Commands.RemoveItem;
using ecommerce.Application.Cqrs.Carts.Queries;
using ecommerce.Application.Cqrs.Carts.Queries.GetQuery;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CartController : BaseController
{
    public CartController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("[action]")]
    
    public async Task<IActionResult> CreateCart([FromBody]CreateCartCommand command)
    {
        var result =await mediator.Send(command);
        return new JsonResult(result);
    }

    [HttpGet("[action]/{Id}")]
    public async Task<IActionResult> Get([FromRoute]GetCartQuery query)
    {
        var response = await mediator.Send(query);
        return NewResult(response);
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> Get([FromRoute]GetCartByUserQuery query)
    {
        var response = await mediator.Send(query);
        return NewResult(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddItem(AddItemCommand command)
    {
        var response = await mediator.Send(command);
        return NewResult(response);
    }
    [HttpDelete("[action]/{Id}")]
    public async Task<IActionResult> RemoveBasketProductItem([FromRoute] RemoveBasketProductItemCommand request)
    {
        var response = await mediator.Send(request);
        return NewResult(response);
    }

    [HttpDelete("[action]/{Id}")]
    public async Task<IActionResult> Delete([FromRoute] GetCartQuery query)
    {
        var response = await mediator.Send(query);
        return NewResult(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CheckOut([FromBody]CheckOutCommand command)
    {
        var response = await mediator.Send(command);
        return NewResult(response);
    }
}