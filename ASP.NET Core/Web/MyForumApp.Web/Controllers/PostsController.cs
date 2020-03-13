namespace MyForumApp.Web.Controllers
{
    using System.Linq;
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
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly UserManager<ApplicationUser> user;
        private readonly ApplicationDbContext dbContext;

        public PostsController(
            IPostsService postsService,
            IDeletableEntityRepository<Post> postsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            UserManager<ApplicationUser> user,
            ApplicationDbContext dbContext)
        {
            this.postsService = postsService;
            this.postsRepository = postsRepository;
            this.categoriesRepository = categoriesRepository;
            this.user = user;
            this.dbContext = dbContext;
        }

        public IActionResult Create()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel inputModel)
        {
            this.postsService.CreatePost<Post>(inputModel.Title, inputModel.Description);

            return this.Redirect("/");
        }
    }
}
