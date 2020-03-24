namespace MyForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
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
