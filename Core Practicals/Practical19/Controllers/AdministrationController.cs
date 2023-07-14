using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practical19.Models;

namespace Practical19.Controllers
{

    public class AdministrationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdministrationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var user = _userManager.Users;
            return View(user);
        }
        public IActionResult CreateRoles()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoles(CreateRoleView createRoleView)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRoleView.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(createRoleView);
        }
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role doesn't exist for specified ID";
                return View("NotFound");
            }
            var model = new EditRoleView
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach(var user in await _userManager.GetUsersInRoleAsync(role.Name))
            {
                model.Users.Add(user.UserName);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleView model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role doesn't exist for specified ID";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsers(string id)
        {
            ViewBag.Id = id;
            var role = await _roleManager.FindByIdAsync(id);

            if(role == null)
            {
                ViewBag.ErrorMessage = "Role doesn't exist for specified ID";
                return View("NotFound");
            }
            var model = new List<UserRoles>();
            foreach(var user in _userManager.Users)
            {
                var userRoles = new UserRoles
                {
                    UserID = user.Id,
                    UserName = user.UserName
                };
                
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoles.IsSelected = true;
                }
                else
                {
                    userRoles.IsSelected = false;
                }
                model.Add(userRoles);
                    
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsers(List<UserRoles> userRoles, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.ErrorMessage = "Role doesn't exist for specified ID";
                return View("NotFound");
            }
            for(int i = 0; i < userRoles.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(userRoles[i].UserID);
                IdentityResult result = null;
                if (userRoles[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!userRoles[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if(result.Succeeded)
                {
                    if(i < (userRoles.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { id = id });
                    }
                }
            }
            return RedirectToAction("EditRole", new { id = id });
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
