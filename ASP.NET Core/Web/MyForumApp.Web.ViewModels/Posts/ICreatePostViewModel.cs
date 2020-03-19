namespace MyForumApp.Web.ViewModels.Posts
{
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;
    using System.Collections.Generic;

    public interface ICreatePostViewModel : IMapTo<Post>
    {
        string Title { get; set; }

        string Description { get; set; }

        int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
