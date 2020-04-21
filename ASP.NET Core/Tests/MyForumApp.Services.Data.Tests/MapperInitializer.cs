namespace MyForumApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;
    using MyForumApp.Web.ViewModels.Categories;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(Category).GetTypeInfo().Assembly,
                typeof(CategoryViewModel).GetTypeInfo().Assembly,
                typeof(PostInCategoryViewModel).GetTypeInfo().Assembly);
        }
    }
}
