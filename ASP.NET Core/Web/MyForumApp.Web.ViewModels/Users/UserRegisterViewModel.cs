namespace MyForumApp.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using MyForumApp.Data.Models;
    using MyForumApp.Services.Mapping;

    public class UserRegisterViewModel : IMapFrom<ApplicationUser>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public string ImageUrl { get; set; }

        public string DefaultImageUrl => "https://www.4dface.io/wp-content/uploads/2018/10/4DFM_sample2.jpg";
    }
}
