using EventMenegmentAdmin.Models;
using EventMenegmentDL.Entity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventMenegmentAdmin.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var allUsers = _userManager.Users.ToList(); // Bütün istifadəçiləri çəkirik
            var usersInRole = new List<UserViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
           
                {
                    usersInRole.Add(new UserViewModel
                    {
                        Name = user.FullName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Role = roles.ToList(),

                    });
                }
            }

            return View(usersInRole);
        }


        public async Task<IActionResult> AssignRole()
        {
           var users = await _userManager.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.FullName,
              
            }).ToListAsync();
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();

           var model= new AssignRoleViewModel
            {
                Users = users,
                Roles = roles
            };
            return View(model);


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError("","User Not Found");
                return View(model);
            }
            var role = await _roleManager.FindByNameAsync(model.RoleName);
            if (role == null)
            {
                ModelState.AddModelError("", "Role Not Found");
                return View(model);
            }
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(model);
            }

        }

     
    }
}
