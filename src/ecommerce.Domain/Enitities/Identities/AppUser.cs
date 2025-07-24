
using Microsoft.AspNetCore.Identity;
namespace ecommerce.Domain.Enitities.Identities;

public class AppUser : IdentityUser<string>
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
}