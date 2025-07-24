using AutoMapper;
using ecommerce.Application.Cqrs.Products.Commands.CreateProduct;
using ecommerce.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommerce.Application.Cqrs.Products.Commands.UpdateProduct;
using ecommerce.Application.Cqrs.Products.queries.Reponses;

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
                })).ReverseMap();
            CreateMap<Product,ProductResponse>()
                .ForMember(p => p.CatgoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
