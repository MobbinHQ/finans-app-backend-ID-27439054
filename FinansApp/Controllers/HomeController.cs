using FinansApp.Business.Users;
using FinansApp.Business.Users.Dto;
using FinansApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinansApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet("giris-yap")]
        public IActionResult Login()
        {
            LoginDto model = new LoginDto();
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost("giris-yap")]
        public async Task<JsonResult> Login(LoginDto model)
        {
            var user = await _userService.Login(model);
            if (user == null)
            {
                return Json(0);
            }
            else
            {
                if (user.UserType == 1)
                {
                    var claims = new List<Claim>{
                            new Claim(ClaimTypes.Name, user.Name + " " + user.Surname),
                            new Claim(ClaimTypes.Email,user.Email),
                            new Claim(ClaimTypes.Sid,user.Id.ToString())
                        };
                    var userIdentity = new ClaimsIdentity(claims, "login");

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(userIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true
                        });
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }

            }
        }
        [HttpGet("cikis")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("/giris-yap");
        }
    }
}
