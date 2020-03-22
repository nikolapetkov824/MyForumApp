namespace MyForumApp.Web.ViewModels.Comments
{
    using System.Collections.Generic;
    using AutoMapper;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public IEnumerable<IndexCommentViewModel> Comments { get; set; }
    }
}
