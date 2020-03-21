namespace MyForumApp.Web.ViewModels.Comments
{
    using System;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class IndexCommentViewModel : IMapFrom<Comment>
    {
        public int PostId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
