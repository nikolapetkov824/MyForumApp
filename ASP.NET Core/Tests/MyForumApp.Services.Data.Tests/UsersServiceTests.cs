namespace MyForumApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.Configuration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using MyForumApp.Data;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Data.Repositories;
    using MyForumApp.Services.Data;
    using MyForumApp.Services.Mapping;
    using MyForumApp.Web.ViewModels.Categories;
    using MyForumApp.Web.ViewModels.Home;
    using MyForumApp.Web.ViewModels.Posts;
    using Xunit;

    public class UsersServiceTests
    {

        /// <summary>
        /// System.NullReferenceException : Object reference not set to an instance of an object.
        /// Expected: MyForumApp.Data.Models.ApplicationUser
        /// Actual:   (null);
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task LoginShouldReturnUserUsingDbContext()
        {
            var password = this.Hash("pass");
            AutoMapperConfig.RegisterMappings(typeof(MyTestApplicationUser).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(ApplicationUser).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var testUser = new ApplicationUser
            {
                UserName = "name",
                PasswordHash = password,
            };
            repository.AddAsync(testUser).GetAwaiter().GetResult();
            repository.AddAsync(new ApplicationUser { UserName = "bob", PasswordHash = password, }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var usersService = new UsersService(repository);
            var user = usersService.Login("bob", password);
            Assert.NotEmpty(repository.All());
            //Assert.IsType<ApplicationUser>(user);
        }

        [Fact]
        public async Task RegisterShouldReturnUserUsingDbContext()
        {
            var password = this.Hash("pass");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new ApplicationUser { Id = "1", UserName = "a", PasswordHash = password }).GetAwaiter().GetResult();
            repository.AddAsync(new ApplicationUser { Id = "2", UserName = "a", PasswordHash = password }).GetAwaiter().GetResult();
            repository.AddAsync(new ApplicationUser { Id = "3", UserName = "a", PasswordHash = password }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var categoriesService = new UsersService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestApplicationUser).Assembly);
            var categories = categoriesService.Register("name", "mail@bg", password, null);
            Assert.Equal("name", categories.UserName);
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

        public class MyTestApplicationUser : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
        {
            public string Id { get; set; }

            public string UserName { get; set; }

            public string PasswordHach { get; set; }

            public string Email { get; set; }
        }

    }
}
