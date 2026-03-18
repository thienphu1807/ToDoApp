using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, AppDbContext appDbContext, SignInManager<User> signInManager)
        {
            _userManager = userManager; 
            _appDbContext = appDbContext;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        // POST: AccountController/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User{ UserName = model.Name, Email = model.EmailAddress };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) 
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login");
            }
            return RedirectToAction("Register");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        // POST: AccountController/login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
          
            var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password,isPersistent:false, lockoutOnFailure:false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult>Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token,"1");
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
