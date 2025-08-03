using AutoMapper;
using ecommerce.Application.Cqrs.Category.Commands.CreateCategory;
using ecommerce.Application.Cqrs.Category.Commands.UpdateCategory;
using ecommerce.Application.Cqrs.Category.Responses;
using ecommerce.Domain.Enitities;

namespace ecommerce.Application.Mappers.CategoruMappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CreateCategoryCommand, Category>().ForMember(c => c.ImageName, opt => opt.MapFrom(cc => cc.File.Name))
            .ForMember(c => c.Name, opt => opt.MapFrom(cc => cc.Name))
            .ForMember(c => c.Description, opt => opt.MapFrom(cc => cc.Description));
        CreateMap<Category, CategoryResponse>();
        CreateMap<UpdateCategoryCommand, Category>()
            .ForMember(c => c.ImageName, opt => opt.MapFrom(cc => cc.FormFile.Name));
    }
}