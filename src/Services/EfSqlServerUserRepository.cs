using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RelativeRank.Services
{
    public class EfSqlServerUserRepository : IUserRepository
    {
        private readonly RelativeRankContext _context;
        private readonly AppSettings _appSettings;
        private readonly IAuthenticationService _authenticationService;

        public EfSqlServerUserRepository(RelativeRankContext context, IOptions<AppSettings> appSettings, IAuthenticationService authenticationService)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _authenticationService = authenticationService;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.User.SingleOrDefaultAsync(u => u.Username == username &&
                _authenticationService.ValidateHash(u.Password, password));

            if (user == null)
            {
                return null;
            }

            var userWithToken = new User()
            {
                Username = username,
                Token = _authenticationService.GenerateJwt(username, _appSettings.Secret)
            };

            return userWithToken;
        }

        public async Task<User> SignUp(string username, string password)
        {
            var userWithUsernameAlready = await _context.User.SingleOrDefaultAsync(u => u.Username == username);
            if (userWithUsernameAlready != null)
            {
                return null;
            }

            var user = new EntityFrameworkEntities.User()
            {
                Username = username,
                Password = _authenticationService.Hash(password)
            };

            _context.Add(user);
            _context.SaveChanges();

            return await Login(username, password);
        }

        public async Task<RankedShowList> GetUsersShows(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUsersShows(User user, RankedShowList updatedList)
        {
            throw new NotImplementedException();
        }
    }
}
