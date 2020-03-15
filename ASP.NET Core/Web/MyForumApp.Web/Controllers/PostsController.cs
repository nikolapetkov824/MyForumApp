namespace MyForumApp.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
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

        public PostsController(
            IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult Create(string id)
        {
            if (this.ModelState.IsValid)
            {
                return this.View();
            }
            else
            {
                return this.View("Error");
            }
        }

        [HttpPost]
        public IActionResult Create(CreatePostViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.postsService.AddPost(model, model.CategoryId).GetAwaiter().GetResult();

                return this.Redirect($"/f/Categories?Id={model.CategoryId}");
            }
            else
            {
                return this.View("Error");
            }
        }
    }
}
