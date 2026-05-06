using BusinessObjects;
using System.Collections.Generic;

namespace DataObjects
{
    public interface IUserDao
    {
        User GetUser(string Username, string Password);

        List<User> GetUsers();

        List<User> GetUsers1();

        List<User> GetUserById(string Username);

        int Update(User user);
    }
}