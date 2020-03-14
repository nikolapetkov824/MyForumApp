namespace MyForumApp.Services.Data
{
    using System.Threading.Tasks;

    using MyForumApp.Data.Models;
    using MyForumApp.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<int> AddPost(ICreatePostViewModel model, int categoryId);
    }
}
