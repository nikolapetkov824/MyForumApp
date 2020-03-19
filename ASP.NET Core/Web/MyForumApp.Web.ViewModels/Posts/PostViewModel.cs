namespace MyForumApp.Web.ViewModels.Posts
{
    using System;

    using Ganss.XSS;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class PostViewModel : IMapFrom<Post>
    {
        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);

        public string UserUserName { get; set; }
    }
}
