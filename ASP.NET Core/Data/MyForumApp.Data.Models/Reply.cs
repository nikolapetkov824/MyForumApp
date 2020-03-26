namespace MyForumApp.Data.Models
{
    using MyForumApp.Data.Common.Models;

    public class Reply : BaseDeletableModel<int>
    {
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
