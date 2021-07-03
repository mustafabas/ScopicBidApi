using BidApp.DataL;
using BidApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BidApp.Service.Users
{
    public class UserService : IUserService
    {
        readonly IRepository<UserEntity> _userRepository;

        public UserService(IRepository<UserEntity> userRepository)
        {
            this._userRepository = userRepository;
        }  

        public UserEntity GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public UserEntity GetByUserNameAndPassword(string userName, string password)
        {
            var query = _userRepository.ListAll();

             return query.FirstOrDefault(x => x.UserName == userName && x.Password==password);
        }

        public UserEntity UpdateUser(UserEntity user)
        {
            _userRepository.Update(user);
            return user;
        }
    }
}
