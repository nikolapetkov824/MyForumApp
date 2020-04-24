namespace MyForumApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }


        /// <summary>
        /// When I try to login with freshly registered user I get
        /// ArgumentNullException: Value cannot be null. (Parameter 'user').
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            var user = this.usersService.Login(
                model.UserName,
                model.Password);

            await this.signInManager.SignInAsync(user, false);

            await this.CreateRolesandUsers(user);

            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (model.ImageUrl == null)
            {
                model.ImageUrl = model.DefaultImageUrl;
            }

            var user = this.usersService.Register(
                model.UserName,
                model.Email,
                model.Password,
                model.ImageUrl);

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.RedirectToAction("Login");
            }
            else
            {
                return this.View(model);
            }
        }

        private async Task CreateRolesandUsers(ApplicationUser user)
        {
            var roleName = "Admin";

            var roleExists = await this.roleManager.RoleExistsAsync(roleName);

            if (roleExists)
            {
                var getUser = await this.userManager.GetUserAsync(this.User);

                if (user.UserName == "Shopov")
                {
                    var getRole = await this.userManager.IsInRoleAsync(user, roleName);
                    if (!getRole)
                    {
                        var result = await this.userManager.AddToRoleAsync(user, roleName);
                    }
                }
                else
                {
                    var getRole = await this.userManager.IsInRoleAsync(user, "User");
                    if (!getRole)
                    {
                        var result = await this.userManager.AddToRoleAsync(user, "User");
                    }
                }
            }
        }
    }
}
