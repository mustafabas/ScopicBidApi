using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BidApp.Service.Users
{
    public class UserService : IUserService
    {
        public List<UserModel> userModels;

        public UserService()
        {
            if (userModels == null)
            {
                userModels = new List<UserModel>();
                userModels.Add(new UserModel
                {
                    userId = 1,
                    userName = "user1"
                });
                userModels.Add(new UserModel
                {
                    userId = 2,
                    userName = "user2"
                });
            }

        }

        public UserModel GetUserById(int id)
        {
            return userModels.First(x => x.userId == id);
        }

        public UserModel getUserByName(string userName)
        {
            return userModels.First(x => x.userName == userName);
        }
    }
}
