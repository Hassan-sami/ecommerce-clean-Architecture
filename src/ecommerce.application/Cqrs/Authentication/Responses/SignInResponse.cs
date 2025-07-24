namespace ecommerce.Application.Cqrs.Authentication.Responses;

public class SignInResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Email { get; set; }
    public JwtResponse response { get; set; }
}