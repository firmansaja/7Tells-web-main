using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionServices
{
    public interface IUserService
    {
        User GetUser(string Username, string Password);
        List<User> GetUsers();
        List<User> GetUsers1();
        List<User> GetUserById(string Username);
        int Update(User user);
    }
}
