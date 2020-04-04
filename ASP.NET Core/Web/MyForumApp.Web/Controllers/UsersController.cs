namespace MyForumApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginViewModel model)
        {
            this.SignIn(ClaimsPrincipal.Current, authenticationScheme: model.UserName);

            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel model)
        {
            this.usersService.Register(model.UserName, model.Email, model.Password, model.ImageUrl);

            return this.RedirectToAction("Login");
        }
    }
}