namespace MyForumApp.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Comments;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        public IActionResult GetById(int postId)
        {
            var viewModel = new CommentViewModel
            {
                Comments = this.commentsService.GetAll<IndexCommentViewModel>()
                    .Where(c => c.PostId == postId).ToList(),
                PostId = postId,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateComment(int postId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Error");
            }

            var viewModel = new CreateCommentViewModel
            {
                PostId = postId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateCommentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var parentId =
                model.CommentParentId == 0 ?
                    (int?)null :
                    model.CommentParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInPostId(parentId.Value, model.PostId))
                {
                    return this.BadRequest();
                }
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.commentsService.CreateAsync(
                model.Content,
                model.PostId,
                user.Id,
                parentId);

            return this.RedirectToAction("ById", "Posts", new { id = model.PostId });
        }
    }
}