namespace MyForumApp.Web.ViewModels.Categories
{
    using System;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {
        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent =>
            this.Content?.Length > 100
                ? this.Content?.Substring(0, 100) + "..."
                : this.Content;

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }
    }
}
