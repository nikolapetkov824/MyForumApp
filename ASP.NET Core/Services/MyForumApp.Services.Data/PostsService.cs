namespace MyForumApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ganss.XSS;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;
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

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            IQueryable<Post> query =
                this.postsRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.CategoryId == categoryId)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByCategoryId2<T>(int categoryId)
        {
            IQueryable<Post> query =
                this.postsRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.CategoryId == categoryId);

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return post;
        }

        public int GetCountByCategoryId(int categoryId)
        {
            return this.postsRepository.All().Count(x => x.CategoryId == categoryId);
        }

        public async Task<int> EditPostContent(int id, string description)
        {
            var post = this.GetById<Post>(id);
            post.Description = new HtmlSanitizer().Sanitize(description);
            this.postsRepository.Update(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }
    }
}
