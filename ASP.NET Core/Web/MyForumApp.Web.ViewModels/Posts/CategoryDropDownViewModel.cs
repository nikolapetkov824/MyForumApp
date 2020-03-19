namespace MyForumApp.Web.ViewModels.Posts
{
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}