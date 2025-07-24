namespace ecommerce.Application.Cqrs.Authentication.Responses;

public class JwtResponse
{
    public string Token { get; set; }
    public RefreshTokenResponse RefreshToken { get; set; }
}