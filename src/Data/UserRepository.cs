using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RelativeRank.Services;
using RelativeRank.DataTransferObjects;

namespace RelativeRank.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly RelativeRankContext _context;

        public UserRepository(RelativeRankContext context) => _context = context;

        public async Task<User> CreateNewUser(EntityFrameworkEntities.User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }

            await _context.User.AddAsync(newUser).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var savedUser = await GetUserByUsername(newUser.Username).ConfigureAwait(false);

            return new User
            {
                Id = savedUser.Id,
                Username = savedUser.Username
            };
        }

        public async Task<User?> UpdateUser(DbUpdateUserModel updateUserModel)
        {
            if (updateUserModel == null ||
                updateUserModel.UserToUpdateCurrentUsername == null ||
                updateUserModel.UserToUpdateNewUsername == null ||
                updateUserModel.Password == null ||
                updateUserModel.PasswordSalt == null)
            {
                throw new ArgumentNullException(nameof(updateUserModel));
            }

            EntityFrameworkEntities.User existingUser;
            try
            {
                 existingUser = await _context.User
                    .SingleAsync(u => u.Username == updateUserModel.UserToUpdateCurrentUsername).ConfigureAwait(false);
            }
            catch
            {
                return null;
            }

            existingUser.Username = updateUserModel.UserToUpdateNewUsername;
            existingUser.Password = updateUserModel.Password;
            existingUser.PasswordSalt = updateUserModel.PasswordSalt;
            _context.Update(existingUser);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            var updatedUser = await GetUserByUsername(existingUser.Username).ConfigureAwait(false);

            return new User
            {
                Id = updatedUser.Id,
                Username = updatedUser.Username
            };
        }

        public async Task<EntityFrameworkEntities.User> GetUserByUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            return await _context.User.SingleOrDefaultAsync(user => user.Username == username).ConfigureAwait(false);
        }

        public async Task<User> DeleteUser(User userToDelete)
        {
            var user = await _context.User.FindAsync(userToDelete.Id).ConfigureAwait(false);

            _context.User.Remove(user);
            _context.SaveChanges();

            return userToDelete;
        }
    }
}
