namespace MyForumApp.Services.Data
{
    using System.Threading.Tasks;

    using MyForumApp.Data.Models;
    using MyForumApp.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<int> CreateAsync(
            string title,
            string description,
            int categoryId,
            string userId);
    }
}
