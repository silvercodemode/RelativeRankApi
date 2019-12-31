﻿using RelativeRank.Entities;
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

            await _context.User.AddAsync(newUser);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var savedUser = await GetUserByUsername(newUser.Username).ConfigureAwait(false);

            return new User
            {
                Id = savedUser.Id,
                Username = savedUser.Username
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