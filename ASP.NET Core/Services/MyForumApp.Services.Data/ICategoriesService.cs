﻿namespace MyForumApp.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name, int? take = null, int skip = 0);
    }
}
