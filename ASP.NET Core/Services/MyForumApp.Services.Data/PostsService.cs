namespace MyForumApp.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(
            string title,
            string description,
            int categoryId,
            string userId)
        {
            var post = new Post
            {
                CreatedOn = DateTime.UtcNow,
                Title = title,
                Description = description,
                CategoryId = categoryId,
                UserId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }
    }
}
