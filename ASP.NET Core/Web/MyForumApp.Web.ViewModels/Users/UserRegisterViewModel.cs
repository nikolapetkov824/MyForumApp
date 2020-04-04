namespace MyForumApp.Web.ViewModels.Users
{
    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class UserRegisterViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string ImageUrl { get; set; }
    }
}
