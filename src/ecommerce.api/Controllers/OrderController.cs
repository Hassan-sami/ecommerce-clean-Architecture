using ecommerce.api.Common;
using ecommerce.Application.Cqrs.Orders.Queries.GetQuery;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController : BaseController
{
    public OrderController(IMediator mediator) : base(mediator)
    {
        
    }
    [HttpGet("[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetOrderByUserId([FromQuery] GetOrderByUserIdQuery request)
    {
        var result = await mediator.Send(request);
        return NewResult(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> TopSeller([FromRoute] GetTopSellersQuery request)
    
    {
            var response = await mediator.Send(request);
            return NewResult(response);
    }
}