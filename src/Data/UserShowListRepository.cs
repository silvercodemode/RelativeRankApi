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

            var showList = _context.UserToShowMapping.Where(userShow => userShow.UserId == userId)
                .Join(
                    _context.Show,
                    userShow => userShow.ShowId,
                    show => show.Id,
                    (userShow, show) => new
                    {
                        Name = show.Name,
                        Rank = userShow.Rank,
                        ShowId = show.Id
                    }
                );

            var usedShowDictionary = new Dictionary<string, UserToShowMapping>();
            foreach (var show in showList)
            {
                if (!usedShowDictionary.ContainsKey(show.Name))
                {
                    usedShowDictionary.Add(show.Name, new UserToShowMapping
                    { 
                        UserId = userId,
                        ShowId = show.ShowId,
                        Rank = show.Rank
                    });
                }
            }

            foreach (var storedShow in rankedShowList)
            {
                if (usedShowDictionary.ContainsKey(storedShow.Name))
                {
                    var showToUpdate = _context.UserToShowMapping
                        .Where(userShow => userShow.UserId == userId &&
                            userShow.ShowId == usedShowDictionary[storedShow.Name].ShowId)
                        .FirstOrDefault();

                    showToUpdate.Rank = storedShow.Rank;
                }
                else
                {
                    var newShow = new UserToShowMapping
                    {
                        UserId = userId,
                        ShowId = _context.Show.Where(show => show.Name == storedShow.Name).FirstOrDefault().Id,
                        Rank = storedShow.Rank
                    };

                    _context.Add(newShow);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
            }

            return rankedShowList;
        }
    }
}
