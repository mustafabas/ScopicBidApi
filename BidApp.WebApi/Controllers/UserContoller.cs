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
            var user = _userService.getUserByName(login.UserName);

            if (user != null)
                response.SetOk(user);

            else
                response.SetNok();

            return Ok(response);
        }
    }
}
