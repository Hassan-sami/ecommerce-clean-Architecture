using System.Security.Claims;
using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Carts.Reponses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Carts.Queries.GetQuery;

public class GetCartQueryHandler(IStringLocalizer<Resource> stringLocalizer, 
                                ICartService cartService,
                                   IMapper mapper, IHttpContextAccessor httpContextAccessor) : ResponseHandler(stringLocalizer),
    IRequestHandler<GetCartQuery, Response<CartResponse>>, IRequestHandler<GetCartByUserQuery, Response<CartResponse>>
{
    public async Task<Response<CartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var result= await cartService.GetCartById(request.Id);
        if (result == null)
            return BadRequest<CartResponse>();
        var response = mapper.Map<CartResponse>(result);
        return Success(response);
    }


    public async Task<Response<CartResponse>> Handle(GetCartByUserQuery request, CancellationToken cancellationToken)
    {
        var user = httpContextAccessor.HttpContext.User;
        if (user == null)
            return BadRequest<CartResponse>();
        var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if(id == null)
            return BadRequest<CartResponse>();
         
        var cart =  await cartService.GetCartById(new Guid(id));
        if (cart is null)
            return BadRequest<CartResponse>();
        var response = mapper.Map<CartResponse>(cart);
        return Success(response);
    }
}