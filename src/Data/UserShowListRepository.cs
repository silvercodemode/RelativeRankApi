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

        public async Task<RankedShowList> UpdateUsersShowList(int userId, RankedShowList rankedShowList)
        {
            if (rankedShowList == null)
            {
                throw new ArgumentNullException(nameof(rankedShowList));
            }

            var nameToShowTable = new Dictionary<string, RankedShow>();
            foreach (var show in rankedShowList)
            {
                nameToShowTable.Add(show.Name, show);
            }

            var showList = _context.UserToShowMapping.Where(userShow => userShow.UserId == userId)
                .Join(
                    _context.Show,
                    userShow => userShow.ShowId,
                    show => show.Id,
                    (userShow, show) => new
                    {
                        Name = show.Name,
                        Rank = userShow.Rank,
                        ShowId = show.Id,
                    }
                );

            var showNameToIdTable = new Dictionary<string, int>();
            var showIdToNameTable = new Dictionary<int, string>();
            foreach (var show in showList)
            {
                showNameToIdTable.Add(show.Name, show.ShowId);
                showIdToNameTable.Add(show.ShowId, show.Name);
            }

            var showsToDelete = _context.UserToShowMapping.Where(userShow => userShow.UserId == userId)
                .AsEnumerable()
                .Where(show => !nameToShowTable.ContainsKey(showIdToNameTable[show.ShowId]));

            _context.RemoveRange(showsToDelete);

            foreach (var show in rankedShowList)
            {
                if (showNameToIdTable.ContainsKey(show.Name))
                {
                    var showToUpdate = _context.UserToShowMapping
                        .Where(userShow => showNameToIdTable.ContainsKey(show.Name) &&
                            userShow.ShowId == showNameToIdTable[show.Name] &&
                            userShow.UserId == userId)
                        .FirstOrDefault();

                    showToUpdate.Rank = show.Rank;
                    showToUpdate.PercentileRank = show.PercentileRank;
                }
                else
                {
                    var newShow = new UserToShowMapping
                    {
                        UserId = userId,
                        ShowId = _context.Show.Where(s => s.Name == show.Name).First().Id,
                        Rank = show.Rank,
                        PercentileRank = show.PercentileRank
                    };

                    _context.Add(newShow);
                }
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return rankedShowList;
        }
    }
}
