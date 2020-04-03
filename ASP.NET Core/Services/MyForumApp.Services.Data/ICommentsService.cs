using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyForumApp.Services.Data
{
    public interface ICommentsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetById<T>(int postId);

        Task CreateAsync(
            string content,
            int postId,
            string userId,
            int? parentId = null);

        bool IsInPostId(int commentId, int postId);
    }
}
