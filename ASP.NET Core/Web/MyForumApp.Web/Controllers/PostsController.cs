namespace MyForumApp.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Posts;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IPostsService postsService,
            UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.userManager = userManager;
        }

        public IActionResult ById(int id)
        {
            return this.View();
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Error");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var postId = await this.postsService.CreateAsync(
                model.Title,
                model.Description,
                model.CategoryId,
                user.Id);

            return this.RedirectToAction(nameof(this.ById), new { id = postId });
        }
    }
}
