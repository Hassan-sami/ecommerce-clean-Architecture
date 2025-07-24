using AutoMapper;
using ecommerce.Application.Base;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Carts.Commands.DeleteCart;

public class DeleteCartCommandHandler(IStringLocalizer<Resource> stringLocalizer, 
                                            IMapper mapper, ICartService cartService) : ResponseHandler(stringLocalizer), IRequestHandler<DeleteCartCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        await cartService.DeleteCart(request.Id);
        return Success("Successfully deleted cart");
    }
}