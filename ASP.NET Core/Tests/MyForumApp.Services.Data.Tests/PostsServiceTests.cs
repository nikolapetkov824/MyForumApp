namespace MyForumApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyForumApp.Data;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Data.Repositories;
    using MyForumApp.Services.Mapping;
    using MyForumApp.Web.ViewModels.Posts;
    using Xunit;

    public class PostsServiceTests
    {
        [Fact]
        public void GetCountByCategoryIdShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Post>>();
            repository.Setup(r => r.All()).Returns(new List<Post>
                                                        {
                                                            new Post() { CategoryId = 3 },
                                                            new Post() { CategoryId = 3 },
                                                            new Post() { CategoryId = 3 },
                                                        }.AsQueryable());
            var service = new PostsService(repository.Object);
            Assert.Equal(3, service.GetCountByCategoryId(3));
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountByCategoryIdShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PostsTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Posts.Add(new Post() { CategoryId = 3 });
            dbContext.Posts.Add(new Post() { CategoryId = 3 });
            dbContext.Posts.Add(new Post() { CategoryId = 3 });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Post>(dbContext);
            var service = new PostsService(repository);
            Assert.Equal(3, service.GetCountByCategoryId(3));
        }

        [Fact]
        public void GetByIdShouldReturnCorrectPost()
        {
            /// I HAVE SPENT ALMOST 10 HOURS TRYING TO MAKE THIS WORK
            /// END RESULT: NOT WORKING !!!
            /// I HAVE TRIED EVERYTHING TO NO AVAIL
            var expected = new Post
            {
                Id = 3,
            };
            var repository = new Mock<IDeletableEntityRepository<Post>>();
            repository.Setup(r => r.All()).Returns(new List<Post>
                                                        {
                                                            new Post() { Id = 3 },
                                                            new Post() { Id = 4 },
                                                            new Post() { Id = 5 },
                                                        }.AsQueryable());
            var service = new PostsService(repository.Object);
            var actual = service.GetById<Post>(3);
            //repository.Verify(x => x.GetById<PostViewModel>(3), Times.Once);
            var post = repository.Object.All().Where(x => x.Id == 3).To<Post>().FirstOrDefault();
            Assert.Equal(expected, post);

        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectPostUsingDbContext()
        {
            var expected = new Post
            {
                Id = 3,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PostsByIdTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Posts.Add(new Post() { Id = 3, Title = "test", });
            dbContext.Posts.Add(new Post() { Id = 4 });
            dbContext.Posts.Add(new Post() { Id = 5 });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Post>(dbContext);
            var service = new PostsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(Post).Assembly);
            var actual = service.GetById<Post>(3);
            Assert.Equal("test", actual.Title);
        }
    }
}
