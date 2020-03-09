namespace MyForumApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyForumApp.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Name, string ImageUrl)>
            {
                ("Sport", "https://tini.to/v8v"),
                ("Coronavirus", "https://tini.to/lTU"),
                ("News", "https://tini.to/rAWT"),
                ("Music", "https://tini.to/Qou"),
                ("Programming", "https://tini.to/ia7"),
                ("Cats", "https://tini.to/fnA"),
                ("Dogs", "https://tini.to/n0g"),
            };
            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category.Name,
                    Description = category.Name,
                    Title = category.Name,
                    ImageUrl = category.ImageUrl,
                });
            }
        }
    }
}
