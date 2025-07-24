using ecommerce.Application.Base;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ecommerce.Application.Cqrs.Products.Commands.DeleteProduct;

public class DeleteProductHandler(IStringLocalizer<Resource> stringLocalizer,
                                    IProductService productService) : ResponseHandler(stringLocalizer), IRequestHandler<DeleteProductByIdCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await productService.DeleteProductByIdAsync(request.Id);
        if (product is null)
            return BadRequest<string>();
        return Success("Deleted product");
    }
}