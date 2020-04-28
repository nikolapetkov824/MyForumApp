namespace MyForumApp.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;

    public partial class LoginModel
    {
        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
    }
}
