using RelativeRank.DataTransferObjects;
using RelativeRank.EntityFrameworkEntities;
using RelativeRank.Interfaces;
using System;
using System.Threading.Tasks;

namespace RelativeRank.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<User> Authenticate(UserAuthentication userAuthentication)
        {
            if (string.IsNullOrEmpty(userAuthentication?.Username) || string.IsNullOrEmpty(userAuthentication?.Password))
            {
                return null;
            }

            var user = await _userRepository.GetUserByUsername(userAuthentication.Username);

            if (user == null || !VerifyPasswordHash(userAuthentication.Password, user.Password, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<NewUser> CreateNewUser(NewUser newUser)
        {
            if (string.IsNullOrEmpty(newUser?.Username) || string.IsNullOrEmpty(newUser?.Password))
            {
                return null;
            }

            byte[] hashedPassword;
            byte[] passwordSalt;
            CreatePasswordHash(newUser.Password, out hashedPassword, out passwordSalt);

            var user = new User
            {
                Username = newUser.Username,
                Password = hashedPassword,
                PasswordSalt = passwordSalt
            };

            var createdUser = await _userRepository.CreateNewUser(user);

            return createdUser;
        }

        // https://jasonwatmore.com/post/2019/10/14/aspnet-core-3-simple-api-for-authentication-registration-and-user-management
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
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
    }
}
