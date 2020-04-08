using Ganss.XSS;
using MyForumApp.Data.Models;
using MyForumApp.Services.Mapping;
using MyForumApp.Web.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForumApp.Web.ViewModels.Posts
{
    public class PostDetailsViewModel : IMapFrom<Post>, IMapTo<Post>
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);

        public string UserUserName { get; set; }

        public int VotesCount { get; set; }

        public int CommentsCount { get; set; }

        public IEnumerable<PostInCategoryViewModel> ForumPosts { get; set; }
    }
}
