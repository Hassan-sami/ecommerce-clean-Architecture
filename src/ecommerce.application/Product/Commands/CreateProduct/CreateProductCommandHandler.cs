using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Application.Product.Commands.CreateProduct
{
    class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Domain.Enitities.Product>
    {
        private IRepository<Domain.Enitities.Product> _repository;
        

        public CreateProductCommandHandler(IRepository<Domain.Enitities.Product> repository)
        {
            this._repository = repository;
        }

        public async Task<Domain.Enitities.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return (await _repository.AddAsync(request.Product));
        }
    }
    
}
