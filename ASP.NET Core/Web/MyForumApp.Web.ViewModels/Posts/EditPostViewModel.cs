namespace MyForumApp.Web.ViewModels.Posts
{
    using Ganss.XSS;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class EditPostViewModel : IMapFrom<Post>, IMapTo<Post>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);
    }
}
