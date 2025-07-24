using ecommerce.Domain.Enitities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommerce.Application.Base;
using ecommerce.Application.Cqrs.Products.queries.Reponses;

namespace ecommerce.Application.Cqrs.Products.queries
{
    public record  PorductGetByIdQuery(Guid id) : IRequest<Response<ProductResponse>>;
}
