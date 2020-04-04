using System;
using System.Collections.Generic;
using System.Text;

namespace MyForumApp.Web.ViewModels.Users
{
    public class UserRegisterViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string ImageUrl { get; set; }

        public int PostId { get; set; }
    }
}
