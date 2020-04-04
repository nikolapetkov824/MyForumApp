namespace MyForumApp.Services.Data
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task Register(
            string name,
            string email,
            string password,
            string imageUrl)
        {
            var passwordHash = this.Hash(password);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = name,
                Email = email,
                PasswordHash = passwordHash,
                ImageUrl = imageUrl,
            };

            await this.usersRepository.AddAsync(user);
            await this.usersRepository.SaveChangesAsync();
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
