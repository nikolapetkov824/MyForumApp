namespace MyForumApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyForumApp.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        [Range(1, int.MaxValue)]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType VoteType { get; set; }
    }
}
