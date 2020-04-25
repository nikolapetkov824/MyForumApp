namespace MyForumApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels;
    using MyForumApp.Web.ViewModels.Posts;

    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IPostsService postsService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult ById(int id)
        {
            var postViewModel = this.postsService.GetById<PostViewModel>(id);
            if (postViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(postViewModel);
        }

        [Authorize]
        public IActionResult CreatePost(ErrorViewModel error)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(error);
            }

            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new CreatePostViewModel
            {
                Categories = categories,
            };

            return this.View(viewModel);
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

        [HttpGet]
        [Authorize]
        public IActionResult EditPost(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Error");
            }

            var viewModel = this.postsService.GetById<PostViewModel>(id);

            //InvalidOperationException: Can't figure out why I can't see the View

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditPost(PostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            //InvalidOperationException: Missing map from Post to Post ???

            var user = await this.userManager.GetUserAsync(this.User);

            var postId = await this.postsService.EditPostContent(model.Id, model.Description);

            return this.RedirectToAction(nameof(this.ById), new { id = postId });
        }
    }
}
