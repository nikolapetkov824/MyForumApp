namespace MyForumApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Data;
    using MyForumApp.Web.ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel voteInputModel)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.votesService.VoteAsync(
                voteInputModel.PostId,
                userId,
                voteInputModel.IsUpVote);

            var votes = this.votesService.GetVotes(voteInputModel.PostId);

            return new VoteResponseModel { VotesCount = votes };
        }
    }
}