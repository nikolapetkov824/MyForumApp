using MyForumApp.Data.Common.Repositories;
using MyForumApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyForumApp.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public Task Login(string name, string email)
        {
            throw new NotImplementedException();
        }

        public async Task Register(string name, string email, string imageUrl)
        {
            var user = new ApplicationUser
            {
                UserName = name,
                Email = email,
                ImageUrl = imageUrl,
            };

            await this.usersRepository.AddAsync(user);
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
