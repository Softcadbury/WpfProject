using AutoMapper;
using DataAccess.ServiceUser;
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
            ServiceUserClient serviceUser = new ServiceUserClient();

            User[] users = serviceUser.GetListUser();
            if (users == null)
                return null;

            Mapper.CreateMap<User, UserModel>();
            return Mapper.Map<List<User>, List<UserModel>>(users.ToList());
        }

        public static UserModel GetUser(string login)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            Mapper.CreateMap<User, UserModel>();
            return Mapper.Map<User, UserModel>(serviceUser.GetUser(login));
        }

        public static bool AddUser(UserModel user)
        {
            ServiceUserClient serviceUser = new ServiceUserClient();
            Mapper.CreateMap<UserModel, User>();
            return serviceUser.AddUser(Mapper.Map<UserModel, User>(user));
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