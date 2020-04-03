namespace MyForumApp.Data.Models
{
    using MyForumApp.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public int? CommentParentId { get; set; }

        public virtual Comment CommentParent { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
