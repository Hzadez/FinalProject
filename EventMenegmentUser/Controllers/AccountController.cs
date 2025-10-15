

using EventMenegmentAdmin.Models;
using EventMenegmentDL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventMenegmentAdmin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
               
                
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                  
                    if (roles.Contains("Admin")|| roles.Contains("Organizer"))
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {

                            CookieOptions options = new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(1),
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict
                            };

                            Response.Cookies.Append("UserName", user.FullName, options);


                            return RedirectToAction("Index", "Home");
                        }

                    }
                    ModelState.AddModelError("", "Yalnız admin istifadəçilər daxil ola bilər.");
                }

                ModelState.AddModelError("", "Giriş uğursuz.");
            }

            return View(model);
        }
    }
}
