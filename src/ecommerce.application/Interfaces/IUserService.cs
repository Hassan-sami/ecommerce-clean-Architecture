using ecommerce.Domain.Enitities.Identities;

namespace ecommerce.Application.Interfaces;

public interface IUserSerivce
{
       public Task<bool> CreateUser(AppUser user,string password);
}