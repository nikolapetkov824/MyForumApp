namespace MyForumApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Posts;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult CreatePost()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreatePost(string title, string description)
        {
            this.postsService.CreatePost<CreatePostViewModel>(title, description);
            return this.Redirect("/");
        }
    }
}
