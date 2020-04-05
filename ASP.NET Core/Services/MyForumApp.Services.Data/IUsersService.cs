using MyForumApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyForumApp.Services.Data
{
    public interface IUsersService
    {
        ApplicationUser Login(
            string name,
            string password);

        ApplicationUser Register(
            string name,
            string email,
            string password,
            string imageUrl);
    }
}
