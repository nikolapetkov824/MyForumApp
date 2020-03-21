using System;
using System.Collections.Generic;
using System.Text;

namespace MyForumApp.Services.Data
{
    public interface ICommentsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int postId);
    }
}
