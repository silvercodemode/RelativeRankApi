using RelativeRank.DataTransferObjects;
using RelativeRank.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using RelativeRank.Config;
using Microsoft.Extensions.Options;
using RelativeRank.Entities;

namespace RelativeRank.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserShowListRepository _userShowListRepository;
        private readonly AppSettings _appSettings;

        public UserService(
            IUserRepository userRepository,
            IUserShowListRepository userShowListRepository,
            IOptions<AppSettings> appSettings
        )
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            _userRepository = userRepository;
            _userShowListRepository = userShowListRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<Entities.User> GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            var user = await _userRepository.GetUserByUsername(username).ConfigureAwait(false);

            return new Entities.User
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public async Task<Entities.User> Authenticate(LoginModel loginInfo)
        {
            if (string.IsNullOrEmpty(loginInfo?.Username) || string.IsNullOrEmpty(loginInfo?.Password))
            {
                return null;
            }

            var user = await _userRepository.GetUserByUsername(loginInfo.Username).ConfigureAwait(false);

            if (user == null || !VerifyPasswordHash(loginInfo.Password, user.Password, user.PasswordSalt))
            {
                return null;
            }

            return new Entities.User
            {
                Id = user.Id,
                Username = user.Username,
                Token = CreateJwt($"{user.Id}")
            };
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private string CreateJwt(string claim)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user", claim)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public async Task<Entities.User> CreateNewUser(SignUpModel newUser)
        {
            if (string.IsNullOrEmpty(newUser?.Username) || string.IsNullOrEmpty(newUser?.Password))
            {
                return null;
            }

            byte[] hashedPassword;
            byte[] passwordSalt;
            CreatePasswordHash(newUser.Password, out hashedPassword, out passwordSalt);

            var user = new EntityFrameworkEntities.User
            {
                Username = newUser.Username,
                Password = hashedPassword,
                PasswordSalt = passwordSalt
            };

            var createdUser = await _userRepository.CreateNewUser(user);

            return new Entities.User
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                Token = CreateJwt($"{createdUser.Id}")
            };
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public RankedShowList GetUsersShowList(int userId)
        {
            return _userShowListRepository.GetUsersShowList(userId);
        }

        public async Task<RankedShowList> UpdateUsersShowList(int userId, RankedShowList rankedShowList)
        {
            return await _userShowListRepository.UpdateUsersShowList(userId, rankedShowList).ConfigureAwait(false);
        }
    }
}
