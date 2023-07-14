using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Practical19.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "User")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
