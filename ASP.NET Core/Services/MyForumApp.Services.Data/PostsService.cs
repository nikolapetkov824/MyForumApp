namespace MyForumApp.Services.Data
{
    using MyForumApp.Data;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public void CreatePost<T>(string title, string description)
        {
            var post = new Post
            {
                Title = title,
                Description = description,
            };

            this.postsRepository.AddAsync(post);
        }
    }
}
