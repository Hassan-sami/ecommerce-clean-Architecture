namespace ecommerce.Application.Cqrs.Authentication.Responses;

public class RefreshTokenResponse
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime CreatedAt { get; set; } 
}