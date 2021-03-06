﻿namespace MyForumApp.Web.ViewModels.Comments
{
    using System;

    using Ganss.XSS;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class IndexCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int? CommentParentId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
