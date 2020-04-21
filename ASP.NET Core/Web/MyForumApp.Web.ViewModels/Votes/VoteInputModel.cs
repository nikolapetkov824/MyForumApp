namespace MyForumApp.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        [Range(1, int.MaxValue)]
        public int PostId { get; set; }

        public bool IsUpVote { get; set; }
    }
}
