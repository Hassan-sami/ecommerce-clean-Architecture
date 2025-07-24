using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities.Identities;
using Microsoft.AspNetCore.Identity;

namespace ecommerce.Application.Services;

public class AuthenticationSerivice : IAuthenticationSerive
{
    private readonly UserManager<AppUser> _userManager;

    public AuthenticationSerivice(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<string> ConfirmEmail(string? userId, string? code)
    {
        if (userId==null||code==null)
            return "ErrorWhenConfirmEmail";
        var user = await _userManager.FindByIdAsync(userId);
        var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
        if (!confirmEmail.Succeeded)
            return "ErrorWhenConfirmEmail";
        return "Success";
    }
}