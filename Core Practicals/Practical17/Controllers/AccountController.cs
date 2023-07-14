using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Practical17.Models;
using Practical17.Repository;
using System.Security.Claims;
using Practical17.Interfaces;

namespace Practical17.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly IUserRepository userRepository;
        public List<User> users = null;
        public AccountController(ILogger<AccountController> logger, IUserRepository _userRepository)
        {
            this.logger = logger;
            this.userRepository = _userRepository;
            users = _userRepository.GetAll();
        }

        public IActionResult Login(string returnUrl = "/")
        {
            LoginModel login = new LoginModel();
            login.ReturnUrl = returnUrl;
            return View(login);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = users.Where(u => u.FirstName == login.UserName && u.Password == login.Password).FirstOrDefault();

            if(user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim (ClaimTypes.NameIdentifier,Convert.ToString(user.UserId)),
                    new Claim (ClaimTypes.Name, user.FirstName),
                    new Claim (ClaimTypes.Role, user.Roles.RoleName),
                    new Claim("RoleBased","Code")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberLogin
                    }

                   );

                return LocalRedirect(login.ReturnUrl);
            }
            else
            {
                ViewBag.Message = "Invalid Credentials";
                return View(login);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
