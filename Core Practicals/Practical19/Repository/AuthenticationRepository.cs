using Microsoft.AspNetCore.Identity;
using Practical19.Interface;
using Practical19.Models;

namespace Practical19.Repository
{
    public class AuthenticationRepository : IAuthentication
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AuthenticationRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);
            return result;

        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAsync(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.UserName);
            if (user != null)
            {
                var identityResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    return identityResult;
                }

            }
            return null;
        }
    }
}
