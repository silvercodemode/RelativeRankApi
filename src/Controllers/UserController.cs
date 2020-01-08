using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

            return Ok(_userService.GetUsersShowList(user.Id));
        }

        [HttpPut("{username}/showlist")]
        public async Task<IActionResult> UpdateUsersShowList([FromBody] UpdateUserShowListModel updateUserShowListModel)
        {
            if (updateUserShowListModel == null)
            {
                return BadRequest("Request body was empty.");
            }

            RankedShowList updatedShowList;
            try
            {
                updatedShowList = new RankedShowList(updateUserShowListModel.ShowList);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            var user = await _userService.GetUserByUsername(updateUserShowListModel.Username).ConfigureAwait(false);

            var requestHasUserClaim = User.Claims.Where(claim => claim.Type == "user" && claim.Value == $"{user.Id}").ToList().Count > 0;
            if (!requestHasUserClaim)
            {
                return BadRequest($"You do not have permissions to update {updateUserShowListModel.Username}'s showlist.");
            }

            var result = await _userService.UpdateUsersShowList(user.Id, new RankedShowList(updatedShowList))
                .ConfigureAwait(false);

            return Ok(result);
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
            
            if (user == null)
            {
                return BadRequest($"User with username: {username} does not exist.");
            }

            return Ok($"Hi {username}");
        }
    }
}