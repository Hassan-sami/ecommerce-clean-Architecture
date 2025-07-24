using ecommerce.Application.Base;
using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities.Identities;
using ecommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Application.Services;

public class UserService(UserManager<AppUser> userManager,
                        IHttpContextAccessor httpContextAccessor,
                            IEmailService emailService,
                        IUrlHelper urlHelper, IUnitOfWork unitOfWork) : IUserSerivce
{
    
    public async Task<bool> CreateUser(AppUser user, string password)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync();
            var result = await userManager.CreateAsync(user,password);
            if (result.Succeeded)
            {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var resquestAccessor = httpContextAccessor.HttpContext.Request;
                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";
                var sent = await emailService.send(user.Email,  "ConFirm Email",message);
                if (!sent)
                {
                    await unitOfWork.RollbackAsync();
                    return false;
                }
                    
            }
        }   
        catch (Exception e)
        {
            await unitOfWork.RollbackAsync();
            return false;
        }
        await unitOfWork.CommitAsync();
        return true;
    }
}