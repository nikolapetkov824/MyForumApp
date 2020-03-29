namespace MyForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyForumApp.Data.Common.Repositories;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class RepliesService : IRepliesService
    {
        private readonly IDeletableEntityRepository<Reply> repliesRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public RepliesService(
            IDeletableEntityRepository<Reply> repliesRepository,
            IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.repliesRepository = repliesRepository;
            this.commentsRepository = commentsRepository;
        }

        public T DeleteReply<T>(int? id = null)
        {
            var reply = this.repliesRepository.All()
                .Where(m => m.Id == id);

            return reply.To<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Reply> query =
                this.repliesRepository.All().OrderBy(x => x.CommentId);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetById<T>(int commentId)
        {
            var comment = this.repliesRepository.All()
                .Where(x => x.CommentId == commentId).To<T>().ToList();

            return comment;
        }

        public async Task ReplyAsync(string content, int commentId, string userId)
        {
            var comment = this.commentsRepository.All()
                .Where(x => x.Id == commentId).FirstOrDefault();

            var reply = new Reply
            {
                Content = content,
                CommentId = comment.Id,
                UserId = userId,
            };

            await this.repliesRepository.AddAsync(reply);
            await this.repliesRepository.SaveChangesAsync();
        }
    }
}
