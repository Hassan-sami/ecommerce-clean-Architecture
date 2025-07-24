namespace ecommerce.Application.Interfaces;

public interface IAuthenticationSerive
{
    public Task<string> ConfirmEmail(string? userId, string? code);
}