namespace MyForumApp.Web.ViewModels.Comments
{
    using System.Collections.Generic;
    using AutoMapper;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;
    using MyForumApp.Web.ViewModels.Replies;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public int PostId { get; set; }

        public IEnumerable<IndexCommentViewModel> Comments { get; set; }
    }
}
