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

    public class VotesServiceTests
    {
        [Fact]
        public async Task TwoDownVotesShouldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfRepository<Vote>(new ApplicationDbContext(options.Options));
            var service = new VotesService(repository);

            for (int i = 0; i < 100; i++)
            {
                await service.VoteAsync(1, "1", false);
            }

            for (int i = 0; i < 100; i++)
            {
                await service.VoteAsync(1, "2", false);
            }

            var votes = service.GetVotes(1);
            Assert.Equal(-2, votes);
        }
    }
}
