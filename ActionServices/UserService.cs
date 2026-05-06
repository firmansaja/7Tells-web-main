using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;

namespace ActionServices
{
    public class UserService : IUserService
    {
        static readonly IDaoFactory factory = DaoFactories.GetFactory();
        static readonly IUserDao userDao = factory.UserDao;
        public User GetUser(string Username, string Password)
        {
            return userDao.GetUser(Username, Password);
        }

        public List<User> GetUserById(string Username)
        {
            return userDao.GetUserById(Username);
        }

        public List<User> GetUsers()
        {
            return userDao.GetUsers();
        }
        public List<User> GetUsers1()
        {
            return userDao.GetUsers1();
        }

        public int Update(User user)
        {
            return userDao.Update(user);
        }
    }
}
