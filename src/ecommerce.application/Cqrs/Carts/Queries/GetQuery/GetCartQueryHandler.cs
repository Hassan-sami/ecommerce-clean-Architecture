using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Carts.Reponses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Carts.Queries.GetQuery;

public class GetCartQueryHandler(IStringLocalizer<Resource> stringLocalizer, 
                                ICartService cartService,
                                   IMapper mapper ) : ResponseHandler(stringLocalizer),IRequestHandler<GetCartQuery, Response<CartResponse>>
{
    public async Task<Response<CartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var result= await cartService.GetCartById(request.Id);
        if (result == null)
            return BadRequest<CartResponse>();
        var response = mapper.Map<CartResponse>(result);
        return Success(response);
    }
}