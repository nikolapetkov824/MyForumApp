namespace MyForumApp.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CreateCommentViewModel : IMapTo<Comment>
    {
        public string Content { get; set; }

        [Range(1, int.MaxValue)]
        public int PostId { get; set; }

        [Range(1, int.MaxValue)]
        public int CommentParentId { get; set; }
    }
}
