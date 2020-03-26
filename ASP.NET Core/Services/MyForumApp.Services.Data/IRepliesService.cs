namespace MyForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepliesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetById<T>(int commentId);

        Task ReplyAsync(
            string content,
            int commentId,
            string userId);
    }
}
