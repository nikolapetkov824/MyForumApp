using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForumApp.Services.Data;
using MyForumApp.Web.ViewModels.Users;

namespace MyForumApp.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Register()
        {
            return this.View("/Users/Register");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Register(UserRegisterViewModel model)
        {
            this.usersService.Register(model.UserName, model.Email, model.ImageUrl);

            return this.RedirectToAction("ById", "Posts", new { id = model.PostId });
        }
    }
}