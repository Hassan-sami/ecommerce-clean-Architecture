using AutoMapper;
using ecommerce.Application.Cqrs.Authentication.Responses;
using ecommerce.Application.Cqrs.Users.Commands.CreateUser;
using ecommerce.Domain.Enitities.Identities;

namespace ecommerce.Application.Mappers.UserMappers;

public class AppUserMapper : Profile
{
    public AppUserMapper()
    {
        CreateMap<RegisterCommand, AppUser>()
            .ForMember(uc => uc.PhoneNumber,
                opt => opt.MapFrom(uc => uc.Phones == null ? string.Empty : uc.Phones.FirstOrDefault()));
        // .ForMember(uc => uc.LastName, opt => opt.MapFrom(uc => uc.LastName))
        // .ForMember(uc => uc.Email, opt => opt.MapFrom(uc => uc.Email));
    }
}