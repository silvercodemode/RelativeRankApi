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
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel newUser)
        {
            var createdUser = await _userService.CreateNewUser(newUser);

            if (createdUser == null)
            {
                return BadRequest(new { message = "Invalid User Info" });
            }

            return Ok(createdUser);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginInfo)
        {
            var userWithToken = await _userService.Authenticate(loginInfo);

            if (userWithToken == null)
            {
                return BadRequest(new { message = "Invalid Username or Password" });
            }

            return Ok(userWithToken);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> Claims()
        {
            var claims = User.Claims.ToList();
            var res = "";

            foreach (var claim in claims)
            {
                res += $"{claim}\n";
            }

            return Ok(res);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> UserDetails(string username)
        {
            var user = await _userService.GetUserByUsername(username);

            var requestHasUserClaim = User.Claims.Where(claim => claim.Type == "user" && claim.Value == $"{user.Id}").ToList().Count > 0;
            if (requestHasUserClaim)
            {
                return Ok($"Hi {username}");
            }

            return BadRequest("Bad credentials.");
        }
    }
}