namespace MyForumApp.Services.Data
{
    public interface IPostsService
    {
        void CreatePost<T>(string title, string description);
    }
}
