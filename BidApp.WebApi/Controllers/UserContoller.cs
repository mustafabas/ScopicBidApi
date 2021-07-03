using BidApp.Service.Users;
using BidApp.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidApp.WebApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserContoller : ControllerBase
    {
        readonly IUserService _userService;

        public UserContoller(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] LoginModel login)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();
            var user = _userService.GetByUserNameAndPassword(login.UserName, login.Password);
            if (user != null)
            {
                UserModel userModel = new UserModel { userId = user.Id, userName = user.UserName };
                response.SetOk(userModel);
            }     
            else
            {
                response.SetNok();
            }
            return Ok(response);
        }
        [HttpPut("{id:int}")]
        public IActionResult put(int id, [FromBody] AmuntModel amount)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var user = _userService.GetUserById(id);
            user.MaxAmount =Convert.ToDecimal(amount.Amount);
            _userService.UpdateUser(user);
            response.SetOk(true);
            return Ok(response);

        }
        [HttpGet]

        [HttpGet]
        public IActionResult get(int id)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();
            var user = _userService.GetUserById(id);
            if (user != null)
            {
                UserModel userModel = new UserModel { userId = user.Id, userName = user.UserName,MaxAmount= Convert.ToInt32(user.MaxAmount)};
                response.SetOk(userModel);
            }
            else
            {
                response.SetNok();
            }
            return Ok(response);
        }
    }
}
