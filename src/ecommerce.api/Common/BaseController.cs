using System.Net;
using ecommerce.Application.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Common;

[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator mediator;

    public BaseController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    #region Actions
    public ObjectResult NewResult<T>(Response<T> response)
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return new OkObjectResult(response);
            case HttpStatusCode.Created:
                return new CreatedResult(string.Empty, response);
            case HttpStatusCode.Unauthorized:
                return new UnauthorizedObjectResult(response);
            case HttpStatusCode.BadRequest:
                return new BadRequestObjectResult(response);
            case HttpStatusCode.NotFound:
                return new NotFoundObjectResult(response);
            case HttpStatusCode.Accepted:
                return new AcceptedResult(string.Empty, response);
            case HttpStatusCode.UnprocessableEntity:
                return new UnprocessableEntityObjectResult(response);
            default:
                return new BadRequestObjectResult(response);
        }
    }
    #endregion

}