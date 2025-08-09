using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Orders.Responses;
using ecommerce.Application.Cqrs.Products.queries.Reponses;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using ecommerce.Domain.Specifications;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Orders.Queries.GetQuery;

public class GetOrderQueryHandler(IStringLocalizer<Resource> stringLocalizer, IOrderService orderService, IMapper mapper) : ResponseHandler(stringLocalizer), 
            IRequestHandler<GetOrderByUserIdQuery,Response<IReadOnlyList<OrderResponse>>>, 
            IRequestHandler<GetTopSellersQuery,Response<IReadOnlyList<ProductResponse>>>
{
    public async Task<Response<IReadOnlyList<OrderResponse>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new OrderWithProductsAndItemsSpec(o => o.UserId == request.UserId,(request.page - 1) * request.size,request.size);
        var orders = await orderService.getOrders(spec);
        var response = (IReadOnlyList<OrderResponse>)orders.Select(p => mapper.Map<OrderResponse>(p)).ToList();
        return Success(response);
    }

    public async Task<Response<IReadOnlyList<ProductResponse>>> Handle(GetTopSellersQuery request, CancellationToken cancellationToken)
    {
        var best = await orderService.GetBestProducts();
        if (best is null || !best.Any())
            return BadRequest<IReadOnlyList<ProductResponse>>("No products found");
        var response = (IReadOnlyList<ProductResponse>)best.Select(p => mapper.Map<ProductResponse>(p)).ToList();
        return Success(response);
    }
}