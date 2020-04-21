namespace MyForumApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    using MyForumApp.Web.ViewModels.Comments;
    using MyForumApp.Web.ViewModels.Home;
    using MyForumApp.Web.ViewModels.Posts;
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public void CreateAsyncShouldReturnCorrectComment()
        {
            AutoMapperConfig.RegisterMappings(typeof(Post).Assembly);
            var commentsRepository = new Mock<IDeletableEntityRepository<Comment>>();
            var postsRepository = new Mock<IDeletableEntityRepository<Post>>();
            commentsRepository.Setup(r => r.All()).Returns(new List<Comment>
                                                        {
                                                            new Comment() { Id = 10 },
                                                            new Comment() { Id = 20 },
                                                            new Comment() { Id = 30 },
                                                        }.AsQueryable());
            var service = new CommentsService(commentsRepository.Object, postsRepository.Object);
            var result = service.CreateAsync("content", 1, "userId", 11);
            Assert.Equal(1, result.Id);
            commentsRepository.Verify(x => x.All(), Times.AtMostOnce);
        }

        [Fact]
        public async Task GetAllShouldReturnAllCommentsUsingDbContext()
        {
            AutoMapperConfig.RegisterMappings(typeof(CommentViewModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(IndexCommentViewModel).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var commentsRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var postsRepository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            var testComment = new Comment
            {
                Id = 1,
            };

            var testPost = new Post
            {
                Id = 10,
            };

            var expectedComment = new IndexCommentViewModel
            {
                Id = 1,
                Content = "content",
                PostId = testPost.Id,
                UserId = "userId",
            };

            var comments = new List<IndexCommentViewModel>();
            comments.Add(expectedComment);

            var expectedComments = new CommentViewModel
            {
                Comments = comments,
                PostId = testPost.Id,
            };

            commentsRepository.AddAsync(testComment).GetAwaiter().GetResult();
            postsRepository.AddAsync(testPost).GetAwaiter().GetResult();
            commentsRepository.SaveChangesAsync().GetAwaiter().GetResult();
            postsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var categoriesService = new CommentsService(commentsRepository, postsRepository);
            var categories = categoriesService.GetAll<CommentViewModel>(1);
            Assert.Single(categories);
        }

        public class MyTestComment : IMapFrom<Comment>, IMapTo<Comment>
        {
            public int Id { get; set; }

            public int PostId { get; set; }

            public int? CommentParentId { get; set; }

            public string Content { get; set; }

            public string UserId { get; set; }
        }

        public class MyTestPost : IMapFrom<Post>, IMapTo<Post>
        {
            public int Id { get; set; }
        }
    }
}
