using BidApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Service.Users
{
    public interface IUserService
    {
        UserEntity GetUserById(int id);
        UserEntity GetByUserNameAndPassword(string userName, string password);

        UserEntity UpdateUser(UserEntity user);

    }
}
