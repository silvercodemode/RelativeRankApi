﻿using System;
using System.Collections.Generic;
using System.Linq;
using RelativeRank.Interfaces;
using RelativeRank.Entities;
using RelativeRank.EntityFrameworkEntities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelativeRank.DataTransferObjects;

namespace RelativeRank.Data
{
    public class ShowRepository : IShowRepository
    {
        private readonly RelativeRankContext _context;
        private readonly IUserShowListRepository _userShowListRepository;

        public ShowRepository(RelativeRankContext context, IUserShowListRepository userShowListRepository)
        {
            _context = context;
            _userShowListRepository = userShowListRepository;
        }

        public async Task<List<RankedShow>> GetAllShowsRelativelyRanked()
        {
            var allShows = _context.UserToShowMapping
                .Join(
                    _context.Show,
                    userShow => userShow.ShowId,
                    show => show.Id,
                    (userShow, show) => new RankedShow
                    {
                        Name = show.Name,
                        PercentileRank = userShow.PercentileRank
                    }
                )
                .GroupBy(rankedShow => rankedShow.Name)
                .Select(rankedShow => new RankedShow
                {
                    Name = rankedShow.Key,
                    PercentileRank = rankedShow.Average(show => show.PercentileRank)
                });

            return await allShows.ToListAsync().ConfigureAwait(false);
        }

        public IEnumerable<Entities.Show> GetAllShows() =>_context.Show.Select(show => new Entities.Show(show.Name));

        public async Task<bool> AddShow(AddShowModel show)
        {
            if (show == null || show.ShowName == null)
            {
                throw new ArgumentNullException(nameof(show));
            }

            var showToAdd = new EntityFrameworkEntities.Show
            {
                Name = show.ShowName
            };

            await _context.Show.AddAsync(showToAdd).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<bool> DeleteShow(int showId)
        {
            var showToDelete = await _context.Show.FindAsync(showId);

            if (showToDelete == null)
            {
                return false;
            }

            var usersWithShowToDeleteInShowList = _context.User.Where(user =>
                _context.UserToShowMapping
                    .Where(userShow => userShow.UserId == user.Id && userShow.ShowId == showId)
                    .Any()).ToList();

            foreach (var user in usersWithShowToDeleteInShowList)
            {
                var showListToRefresh = _userShowListRepository.GetUsersShowList(user.Id);
                showListToRefresh.Remove(showToDelete.Name);

                await _userShowListRepository.SetUsersShowList(user.Id, showListToRefresh).ConfigureAwait(false);
            }

            _context.Show.Remove(showToDelete);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}
