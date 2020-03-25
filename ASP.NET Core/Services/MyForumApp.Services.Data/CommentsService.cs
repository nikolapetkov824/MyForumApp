namespace MyForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public CommentsService(
            IDeletableEntityRepository<Comment> commentsRepository,
            IDeletableEntityRepository<Post> postsRepository)
        {
            this.commentsRepository = commentsRepository;
            this.postsRepository = postsRepository;
        }

        public async Task CreateAsync(
            string content,
            int postId,
            string userId)
        {
            var post = this.postsRepository.All().Where(x => x.Id == postId).FirstOrDefault();
            var comment = new Comment
            {
                Content = content,
                PostId = post.Id,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Comment> query =
                this.commentsRepository.All().OrderBy(x => x.PostId);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetById<T>(int postId)
        {
            var comment = this.commentsRepository.All().Where(x => x.PostId == postId).To<T>().ToList();

            return comment;
        }
    }
}
