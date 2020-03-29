namespace MyForumApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Replies;

    public class RepliesController : Controller
    {
        private readonly IRepliesService repliesService;
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public RepliesController(
            IRepliesService repliesService,
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.repliesService = repliesService;
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public IActionResult GetById(int commentId, int postId)
        {
            var viewModel = new ReplyViewModel
            {
                Replies = this.repliesService.GetAll<IndexReplyViewModel>()
                    .Where(c => c.CommentId == commentId).ToList(),
                CommentId = commentId,
                PostId = postId,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateReply(int commentId, int postId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Error");
            }

            var viewModel = new CreateReplyViewModel
            {
                CommentId = commentId,
                PostId = postId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReply(CreateReplyViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.repliesService.ReplyAsync(
                model.Content,
                model.CommentId,
                user.Id);

            return this.RedirectToAction(nameof(this.GetById), new { commentId = model.CommentId, postId = model.PostId });
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.View("Error");
            }

            var reply = this.repliesService.DeleteReply<Reply>(id);

            this.dbContext.Replies.Remove(reply);
            this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.GetById));
        }
    }
}