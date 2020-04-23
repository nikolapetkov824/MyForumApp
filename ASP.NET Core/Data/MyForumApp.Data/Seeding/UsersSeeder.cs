namespace MyForumApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyForumApp.Common;
    using MyForumApp.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(roleManager, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                var result = await userManager.CreateAsync(new ApplicationUser());
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
