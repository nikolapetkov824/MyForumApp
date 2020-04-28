namespace MyForumApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Categories;
    using MyForumApp.Web.ViewModels.Posts;

    [Authorize]
    public class PostDetailsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public PostDetailsController(
            IPostsService postsService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.postsService = postsService;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Details(int postId, int categoryId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Error");
            }

            var post = this.postsService.GetById<PostDetailsViewModel>(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            post.ForumPosts = this.postsService.GetByCategoryIdWithoutSkip<PostInCategoryViewModel>(categoryId);

            return this.View(post);
        }
    }
}
