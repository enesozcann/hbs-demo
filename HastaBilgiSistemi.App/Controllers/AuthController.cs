using AutoMapper;
using HastaBilgiSistemi.App.Helpers.Abstract;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userLoginDto.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    var role = await _userManager.GetRolesAsync(user);
                    if (result.Succeeded)
                    {
                        if (role.Any(r=>r.Equals("Admin")))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else if (role.Any(r => r.Equals("Doctor")))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Doctor" });
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adınız ya da parolanız yanlıştır.");
                        return View("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adınız ya da parolanız yanlıştır.");
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Auth", new { Area = "" });
        }
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }

    }
}
