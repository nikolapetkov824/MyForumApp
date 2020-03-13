namespace MyForumApp.Services.Data
{
    using MyForumApp.Data;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        //private readonly ApplicationUser user;
        //private readonly Category category;

        public PostsService(
            IDeletableEntityRepository<Post> postsRepository)
            //ApplicationUser user,
           // Category category)
        {
            this.postsRepository = postsRepository;
            //this.user = user;
            //this.category = category;
        }

        public void CreatePost<T>(string title, string description)
        {
            //var userId = this.user.Id;
            //var categoryId = this.category.Id;

            var post = new Post
            {
                Title = title,
                Description = description,
                //UserId = userId,
                //CategoryId = categoryId,
            };

            this.postsRepository.AddAsync(post); /*CreatePost<CreatePostViewModel>(title, description);*/
            this.postsRepository.SaveChangesAsync();
        }
    }
}
