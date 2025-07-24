namespace ecommerce.Application.Cqrs.Authentication.Responses;

public class AppUserCreated
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}