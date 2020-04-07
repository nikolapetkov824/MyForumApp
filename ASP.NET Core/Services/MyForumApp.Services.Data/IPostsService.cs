﻿namespace MyForumApp.Services.Data
{
    using System.Collections.Generic;
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

        T GetById<T>(int id);

        T GetPostDetails<T>(int postId);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}
