namespace MyForumApp.Web.ViewModels.Posts
{
    using Ganss.XSS;
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class EditPostViewModel : IMapFrom<Post>, IMapTo<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);
    }
}
