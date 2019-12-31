using RelativeRank.Entities;
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

            var currentList = GetUsersShowList(userId);

            var usedShowDictionary = new Dictionary<string, RankedShow>();

            throw new NotImplementedException();
        }
    }
}
