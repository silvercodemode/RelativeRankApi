using RelativeRank.Services;
using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace RelativeRank.Services
{
    public class EfSqlServerUserRepository : IUserRepository
    {
        private readonly RelativeRankContext _context;
        private readonly AppSettings _appSettings;

        public EfSqlServerUserRepository(RelativeRankContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // this code adapted from: https://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api
        public User Login(string username, string password)
        {
            var user = _context.User.SingleOrDefault(u => u.Username == username && u.Password == HashPassword(password));

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userWithToken = new User()
            {
                Username = username,
                Token = tokenHandler.WriteToken(token)
            };

            return userWithToken;
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public User SignUp(string username, string password)
        {
            var userWithUsernameAlready = _context.User.SingleOrDefault(u => u.Username == username);
            if (userWithUsernameAlready != null)
            {
                return null;
            }

            var user = new EntityFrameworkEntities.User()
            {
                Username = username,
                Password = HashPassword(password)
            };

            _context.Add(user);
            _context.SaveChanges();

            return Login(username, password);
        }

        public RankedShowList GetUsersShows(string username)
        {
            throw new NotImplementedException();
        }

        public void UpdateUsersShows(string username, RankedShowList updatedList)
        {
            throw new NotImplementedException();
        }
    }
}
