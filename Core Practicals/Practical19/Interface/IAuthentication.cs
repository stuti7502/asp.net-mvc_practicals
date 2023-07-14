using Microsoft.AspNetCore.Identity;
using Practical19.Models;
using Microsoft.AspNetCore.Mvc;

namespace Practical19.Interface
{
    public interface IAuthentication
    {
        Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAsync(LoginModel login);

        Task<IdentityResult> RegisterAsync(RegisterModel register);
    }
}
