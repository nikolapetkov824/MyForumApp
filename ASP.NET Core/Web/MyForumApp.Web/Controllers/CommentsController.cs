namespace MyForumApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Comments;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public IActionResult GetById(int postId)
        {
            var viewModel = new CommentViewModel
            {
                Comments = this.commentsService.GetById<IndexCommentViewModel>(postId),
            };

            return this.View(viewModel);
        }

        public IActionResult AddComment()
        {
            //var userId = this.userManager.GetUserId(this.User);

            //await this.votesService.VoteAsync(
            //    voteInputModel.PostId,
            //    userId,
            //    voteInputModel.IsUpVote);

            //var votes = this.votesService.GetVotes(voteInputModel.PostId);

            //return new VoteResponseModel { VotesCount = votes };
            return this.View();
        }
    }
}