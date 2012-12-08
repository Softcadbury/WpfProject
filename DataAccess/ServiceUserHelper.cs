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

            try
            {
                return serviceUser.GetListUser();
            }
            catch (Exception)
            {
                return new User[0];
            }
        }

        public static User GetUser(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();

            try
            {
                return serviceUser.GetUser(login);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool AddUser(User user)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();

            try
            {
                return serviceUser.AddUser(user);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteUser(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();

            try
            {
                return serviceUser.DeleteUser(login);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Connect(string login, string pwd)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();

            try
            {
                return serviceUser.Connect(login, pwd);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Disconnect(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();

            try
            {
                serviceUser.Disconnect(login);
            }
            catch (Exception)
            { }
        }

        public static string GetRole(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();

            try
            {
                return serviceUser.GetRole(login);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}