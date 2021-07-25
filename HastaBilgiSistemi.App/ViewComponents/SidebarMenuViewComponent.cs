using HastaBilgiSistemi.App.Models;
using HastaBilgiSistemi.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.ViewComponents
{
    public class SidebarMenuViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public SidebarMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ViewViewComponentResult Invoke()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            return View(new UserViewModel
            {
                User = user,
                Roles = roles
            });
        }
    }
}
