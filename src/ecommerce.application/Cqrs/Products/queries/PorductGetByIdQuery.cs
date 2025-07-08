using ecommerce.Domain.Enitities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Application.Cqrs.Products.queries
{
    public record class PorductGetByIdQuery(Guid id) : IRequest<Product>;
}
