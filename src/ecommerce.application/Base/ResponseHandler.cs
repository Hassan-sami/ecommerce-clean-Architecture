using ecommerce.Application.Resources;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Base;

public class ResponseHandler(IStringLocalizer<Resource> stringLocalizer)
{
    

    public Response<T> Deleted<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = message == null ? "" : message
        };
    }

    public Response<T> Success<T>(T entity, object? meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = stringLocalizer[LocalizationConstants.Success],
            Meta = meta
        };
    }

    public Response<T> Unauthorized<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Succeeded = true,
            Message = message == null ? "" : message
        };
    }

    public Response<T> BadRequest<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = message == null ? "" : message
        };
    }

    public Response<T> UnprocessableEntity<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = message == null ? "" : message
        };
    }


    public Response<T> NotFound<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,
            Succeeded = false,
            Message = message == null ? "" : message
        };
    }

    public Response<T> Created<T>(T entity, object? meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.Created,
            Succeeded = true,
            Message = stringLocalizer[LocalizationConstants.Created],
            Meta = meta
        };
    }
}

