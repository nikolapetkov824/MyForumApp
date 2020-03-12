namespace MyForumApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Posts;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsController(IPostsService postsService, IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsService = postsService;
            this.postsRepository = postsRepository;
        }

        public IActionResult CreatePost()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string title, string description)
        {
            var post = new Post
            {
                Title = title,
                Description = description,
                IsDeleted = false,
            };
            await this.postsRepository.AddAsync(post); /*CreatePost<CreatePostViewModel>(title, description);*/
            return this.Redirect("/");
        }
    }
}
