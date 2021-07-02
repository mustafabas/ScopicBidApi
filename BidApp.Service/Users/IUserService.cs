using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Service.Users
{
    public interface IUserService
    {
        UserModel GetUserById(int id);
        UserModel getUserByName(string userName);

    }
}
