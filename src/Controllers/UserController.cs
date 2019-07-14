using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelativeRank.Entities;
using RelativeRank.Interfaces;

// this code adapted from: https://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api
namespace RelativeRank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserRepository _userService;

        public UserController(IUserRepository userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var userWithToken = _userService.Login(user.Username, user.Password);

            if (userWithToken == null)
            {
                return BadRequest(new { message = "Invalid Username or Password" });
            }

            return Ok(user);
        }
    }
}