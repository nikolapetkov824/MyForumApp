namespace MyForumApp.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
