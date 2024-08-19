using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SalesTransactionApp.Services.Interface;
using System.Security.Claims;
using SalesTransactionApp.Models;

namespace SalesTransactionApp.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly IUserService _userService;

        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                var isValidUser = await _userService.ValidateUserCredentialsAsync(model.LogId, model.Password);

                if (isValidUser)
                {
                    var user = await _userService.GetUserByLoginIdAsync(model.LogId);


                    var claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Name, user.UserName),
                         new Claim(ClaimTypes.NameIdentifier, user.LogId.ToString()),
                         new Claim(ClaimTypes.Role, user.Role)
                   };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }


    }
}
