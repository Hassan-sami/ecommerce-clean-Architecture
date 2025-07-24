namespace ecommerce.Application.Interfaces;

public interface IEmailService
{
    public Task<bool> send(string to, string subject, string message);
}