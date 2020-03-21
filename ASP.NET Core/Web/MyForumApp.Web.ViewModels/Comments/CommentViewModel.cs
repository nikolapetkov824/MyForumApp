namespace MyForumApp.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CommentViewModel
    {
        public IEnumerable<IndexCommentViewModel> Comments { get; set; }
    }
}
