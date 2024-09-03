using MaLacoste_Footwear.Models;
using MaLacoste_Footwear.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaLacoste_Footwear.Controllers
{
    [Authorize] // blocks everything
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        private readonly string _role = "Customer";

        public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginModel) // that async word is important
        {
            if (ModelState.IsValid)
            {
                AppUser user =
                  await _userManager.FindByNameAsync(loginModel.UserName);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,
                      loginModel.Password, isPersistent: loginModel.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Home/Index");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(loginModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                if (await _roleManager.FindByNameAsync(_role) is null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(_role));
                }

                var user = new AppUser
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email
                };

                //code for adding avatar image
                if (registerModel.AvatarImage is not null && registerModel.AvatarImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await registerModel.AvatarImage.CopyToAsync(memoryStream);
                        user.AvatarImage = memoryStream.ToArray();
                    }
                }
                else
                {
                    user.AvatarImage = null;
                }

                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, _role);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (IdentityError error in result.Errors) 
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerModel);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

