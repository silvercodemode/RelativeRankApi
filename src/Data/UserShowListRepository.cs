using RelativeRank.Entities;
using RelativeRank.EntityFrameworkEntities;
using RelativeRank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.Data
{
    public class UserShowListRepository : IUserShowListRepository
    {
        private readonly RelativeRankContext _context;

        public UserShowListRepository(RelativeRankContext context) => _context = context;

        public RankedShowList GetUsersShowList(int userId)
        {
            var showList = _context.UserToShowMapping.Where(userShow => userShow.UserId == userId)
                .Join(
                    _context.Show,
                    userShow => userShow.ShowId,
                    show => show.Id,
                    (userShow, show) => new RankedShow
                    {
                        Name = show.Name,
                        Rank = userShow.Rank
                    }
                );

            return new RankedShowList(showList);
        }

        public async Task<RankedShowList> SetUsersShowList(int userId, RankedShowList rankedShowList)
        {
            if (rankedShowList == null)
            {
                throw new ArgumentNullException(nameof(rankedShowList));
            }

            _context.RemoveRange(_context.UserToShowMapping.Where(userShow => userShow.UserId == userId));

            await _context.SaveChangesAsync().ConfigureAwait(false);

            _context.AddRange(rankedShowList.Select(show => new UserToShowMapping
            {
                UserId = userId,
                ShowId = _context.Show.Where(s => s.Name == show.Name).First().Id,
                Rank = show.Rank,
                PercentileRank = show.PercentileRank
            }));

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return rankedShowList;
        }

        public void DeleteUsersShowList(int userId)
        {
            var userToShowMappingsToDelete = _context.UserToShowMapping.Where(userShow => userShow.UserId == userId);

            _context.RemoveRange(userToShowMappingsToDelete);
            _context.SaveChanges();
        }
    }
}
