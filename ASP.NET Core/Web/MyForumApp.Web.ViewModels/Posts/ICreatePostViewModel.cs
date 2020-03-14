namespace MyForumApp.Web.ViewModels.Posts
{
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public interface ICreatePostViewModel : IMapTo<Post>
    {
        string Title { get; set; }

        string Description { get; set; }

        int CategoryId { get; set; }

        string CategoryName { get; set; }
    }
}
