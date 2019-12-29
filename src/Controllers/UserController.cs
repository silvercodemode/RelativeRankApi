using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RelativeRank.DataTransferObjects;
using RelativeRank.Entities;
using RelativeRank.Interfaces;

namespace RelativeRank.Controllers
{
    [Route("user")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        //[AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] NewUser newUser)
        {
            var createdUser = await _userService.CreateNewUser(newUser);

            if (createdUser == null)
            {
                return BadRequest(new { message = "Invalid Username or Password" });
            }

            return Ok(createdUser);
        }
    }
}