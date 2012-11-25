using DataAccess.ServiceUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ServiceUserHelper
    {
        public static User[] GetListUser()
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            return serviceUser.GetListUser();
        }

        public static User GetUser(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            return serviceUser.GetUser(login);
        }

        public static bool AddUser(User user)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            return serviceUser.AddUser(user);
        }

        public static bool DeleteUser(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            return serviceUser.DeleteUser(login);
        }

        public static bool Connect(string login, string pwd)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            return serviceUser.Connect(login, pwd);
        }

        public static void Disconnect(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            serviceUser.Disconnect(login);
        }

        public static string GetRole(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            return serviceUser.GetRole(login);
        }
    }
}