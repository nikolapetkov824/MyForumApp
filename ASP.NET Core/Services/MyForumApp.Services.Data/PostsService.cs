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

        public async Task<int> AddPost(ICreatePostViewModel model, int categoryId)
        {
            var post = new Post
            {
                CreatedOn = DateTime.UtcNow,
                Title = model.Title,
                Description = model.Description,
                CategoryId = categoryId,
            };

            await this.postsRepository.AddAsync(post);
            return await this.postsRepository.SaveChangesAsync();
        }
    }
}
