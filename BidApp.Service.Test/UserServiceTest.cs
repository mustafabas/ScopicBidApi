
using BidApp.DataL;
using BidApp.Entities;
using BidApp.Service.Users;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Service.Test
{
    public class UserServiceTest
    {
        IUserService _userService;
        Mock<IRepository<UserEntity>> _userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IRepository<UserEntity>>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Test]
        public void Should_Get_UserById()
        {
            UserEntity userMock = new UserEntity();
            userMock.Id = 1;
            userMock.MaxAmount = 10;
            userMock.UserName = "user1";
            userMock.Password = "123456";
            _userRepositoryMock.Setup(p => p.GetById(1)).Returns(userMock);
            var actualUser = _userService.GetUserById(1);
            Assert.AreEqual(userMock, actualUser);


        }

        [Test]
        public void Should_GetUser_ByUserNamePassword()
        {
            List<UserEntity> users = new List<UserEntity>();

            UserEntity userMock = new UserEntity();
            userMock.Id = 1;
            userMock.MaxAmount = 10;
            userMock.UserName = "user1";
            userMock.Password = "123456";
            users.Add(userMock);
            _userRepositoryMock.Setup(p => p.ListAll()).Returns(users);

            var actualUser = _userService.GetByUserNameAndPassword("user1", "123456");
            Assert.AreEqual(userMock, actualUser);


        }

    }

}