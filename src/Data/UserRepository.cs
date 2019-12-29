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

        public async Task<User> CreateNewUser(RelativeRank.EntityFrameworkEntities.User newUser)
        {
            await _context.User.AddAsync(newUser);
            await _context.SaveChangesAsync();
            var savedUser = await GetUserByUsername(newUser.Username);

            return new User
            {
                Id = savedUser.Id,
                Username = savedUser.Username
            };
        }

        public async Task<RelativeRank.EntityFrameworkEntities.User> GetUserByUsername(string username)
        {
            return await _context.User.SingleOrDefaultAsync(user => user.Username == username);
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
