using CustomIdentity.Enums;
using CustomIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CustomIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (ModelState.IsValid)
            {
                var user = new AppUser(model.Name, model.Email, model.Password);
                _userManager.AddToRoleAsync(user, UserRoles.user.ToString());
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (ModelState.IsValid)
            {
                var user = new AppUser(model.Email, model.Password);
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (_userManager.IsInRoleAsync(user, "admin").Result)
                    {
                        return RedirectToAction("AdminPage", "Home");
                    }
                    if (_userManager.IsInRoleAsync(user, "user").Result)
                    {
                        return RedirectToAction("UserPage", "Home");
                    }
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
