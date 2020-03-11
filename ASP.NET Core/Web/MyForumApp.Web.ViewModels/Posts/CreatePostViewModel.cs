namespace MyForumApp.Web.ViewModels.Posts
{
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CreatePostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
