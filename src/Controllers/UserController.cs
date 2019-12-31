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
            var createdUser = await _userService.CreateNewUser(newUser).ConfigureAwait(false);

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
            var userWithToken = await _userService.Authenticate(loginInfo).ConfigureAwait(false);

            if (userWithToken == null)
            {
                return BadRequest(new { message = "Invalid Username or Password" });
            }

            return Ok(userWithToken);
        }

        [HttpGet("{username}/showlist")]
        public async Task<IActionResult> GetUserShowList(string username)
        {
            var user = await _userService.GetUserByUsername(username).ConfigureAwait(false);

            var requestHasUserClaim = User.Claims.Where(claim => claim.Type == "user" && claim.Value == $"{user.Id}").ToList().Count > 0;
            if (!requestHasUserClaim)
            {
                return BadRequest("Bad credentials.");
            }

            var list = new RankedShowList();

            list.Add(new RankedShow
            {
                Name = "Love Live",
                Rank = 2
            });

            list.Add(new RankedShow
            {
                Name = "Eva",
                Rank = 1
            });

            list.Add(new RankedShow
            {
                Name = "Kaiba",
                Rank = 3
            });

            return Ok(list);
        }

        [HttpGet("claims")]
        public IActionResult Claims()
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
            var user = await _userService.GetUserByUsername(username).ConfigureAwait(false);

            var requestHasUserClaim = User.Claims.Where(claim => claim.Type == "user" && claim.Value == $"{user.Id}").ToList().Count > 0;
            if (!requestHasUserClaim)
            {
                return BadRequest("Bad credentials.");
            }
            
            return Ok($"Hi {username}");
        }
    }
}