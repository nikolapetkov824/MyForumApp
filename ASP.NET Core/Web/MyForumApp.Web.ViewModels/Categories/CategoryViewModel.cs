namespace MyForumApp.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public string SortBy { get; set; }

        public IEnumerable<PostInCategoryViewModel> ForumPosts { get; set; }
    }
}
