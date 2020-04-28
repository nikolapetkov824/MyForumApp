namespace MyForumApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyForumApp.Data;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Data.Repositories;
    using MyForumApp.Services.Data;
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
        public async Task GetByIdShouldReturnCorrectPostUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Post { Id = 3, Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new PostsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById<MyTestPost>(3);

            Assert.Equal("test", post.Title);
        }

        [Fact]
        public async Task GetByCategoryIdShouldReturnCorrectPostsUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Post { Id = 3, CategoryId = 1, Title = "test" }).GetAwaiter().GetResult();
            repository.AddAsync(new Post { Id = 4, CategoryId = 1, Title = "b" }).GetAwaiter().GetResult();
            repository.AddAsync(new Post { Id = 5, CategoryId = 2, Title = "c" }).GetAwaiter().GetResult();
            repository.AddAsync(new Post { Id = 6, CategoryId = 2, Title = "d" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new PostsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var posts = postService.GetByCategoryId<MyTestPost>(1);

            Assert.Equal(2, posts.Count());
        }

        /// <summary>
        /// I have no idea how to test this method.
        /// Maybe I need to implement another method for
        /// finding Posts ,but using a different parameter,
        /// different from PostId.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateAsyncShouldReturnCreatedPostIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Post { Id = 3, CategoryId = 1, Title = "test" }).GetAwaiter().GetResult();
            repository.AddAsync(new Post { Id = 4, CategoryId = 1, Title = "b" }).GetAwaiter().GetResult();
            repository.AddAsync(new Post { Id = 5, CategoryId = 2, Title = "c" }).GetAwaiter().GetResult();
            repository.AddAsync(new Post { Id = 6, CategoryId = 2, Title = "d" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new PostsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.CreateAsync("title", "description", 1, "userId");
            //var byCategoryId = postService.GetByCategoryId<MyTestPost>(1);
            //var findPost = byCategoryId.Where(x => x.Title == "title").FirstOrDefault();

            Assert.Equal(1, post.Id);
        }

        [Fact]
        public async Task EditPostContentShouldReturnCreatedPostIdUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Post { Id = 1, CategoryId = 2, Title = "test", Description = "notChanged" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new PostsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.EditPostContent(1, "description");
            //var byCategoryId = postService.GetByCategoryId<MyTestPost>(1);
            //var findPost = byCategoryId.Where(x => x.Title == "title").FirstOrDefault();

            Assert.Equal(1, post.Id);
            //Assert.Equal("description", post.GetType().FullName);
        }

        public class MyTestPost : IMapFrom<Post>
        {
            public int Id { get; set; }

            public int CategoryId { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }
        }
    }
}
