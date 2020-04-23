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
                ("Sport", "https://tinyurl.com/y6vnfzhf"),
                ("Coronavirus", "https://tinyurl.com/ycbgyoch"),
                ("News", "https://tinyurl.com/yx47x3no"),
                ("Music", "https://tinyurl.com/y7979n33"),
                ("Programming", "https://tinyurl.com/y785qopu"),
                ("Cats", "https://tinyurl.com/rjvll5q"),
                ("Dogs", "https://tinyurl.com/sgxels3"),
                ("Video Games", "https://tinyurl.com/y9spk64y"),
                ("Cars", "https://tinyurl.com/y7lugo6l"),
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
