namespace MyForumApp.Web.ViewModels.Comments
{
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CreateCommentViewModel : IMapTo<Comment>
    {
        public string Content { get; set; }

        public int PostId { get; set; }
    }
}
