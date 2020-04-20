namespace MyForumApp.Data.Models
{
    using System.Collections.Generic;
    using AutoMapper.Configuration;
    using MyForumApp.Data.Common.Models;
    using MyForumApp.Services.Mapping;

    public class Category : BaseDeletableModel<int>, IMapTo<Category>
    {
        /// <summary>
        /// INVALIDOPERATIONEXCEPTION IS KILLING ME
        /// I have no idea how to make this CreateMap<> work
        /// </summary>
        public Category()
        {
            this.Posts = new HashSet<Post>();
            //var config = new MapperConfigurationExpression();
            //config.CreateMap<Category, Category>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
