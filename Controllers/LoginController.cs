using Bussiness.Abstract;
using Bussiness.Concrate;
using DataAccess.Abstract;
using DataAccess.Concrate;
using Entity.Concrate;
using FormApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FormApp.Controllers
{
    public class LoginController : Controller
    {
		private IUserService _userService;	
        public LoginController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public  IActionResult Login(UserLogInViewModel p)
		{
				var result = _userService.CheckLogin(p.Username,p.Password);	
				
				if (result.Key)
				{
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,p.Username),
                new Claim("Username", p.Username),
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim("Id", result.Value),
            };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity) );
                return RedirectToAction("Index", "Form");
				}
			 return View();
		}
	}
}
