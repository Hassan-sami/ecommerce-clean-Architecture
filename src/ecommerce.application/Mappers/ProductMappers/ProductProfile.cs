using AutoMapper;
using ecommerce.Application.Cqrs.Products.Commands.CreateProduct;
using ecommerce.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Application.Mappers.ProductMappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category
                {
                    Name = src.CatName,
                    Description = src.CatDescription,
                    ImageName = src.CatImageName
                }));
        }
    }
}
