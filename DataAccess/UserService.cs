using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class UserService
    {
        public static List<UserModel> GetListUser()
        {
            return new List<UserModel>();
        }

        public static UserModel GetUser(string login)
        {
            return new UserModel();
        }

        public static bool AddUser(UserModel user)
        {
            return true;
        }

        public static bool DeleteUser(string login)
        {
            return true;
        }

        public static bool Connect(string login, string pwd)
        {
            return true;
        }

        public static void Disconnect(string login)
        {
        }

        public static string GetRole(string login)
        {
            return "roumain";
        }
    }
}