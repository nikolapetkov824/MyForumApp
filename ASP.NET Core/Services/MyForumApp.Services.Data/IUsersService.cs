using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyForumApp.Services.Data
{
    public interface IUsersService
    {
        Task Login(string name, string email);

        Task Register(string name, string email, string imageUrl);
    }
}
