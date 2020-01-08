using System;
using System.Collections.Generic;
using System.Linq;
using RelativeRank.Interfaces;
using RelativeRank.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RelativeRank.Data
{
    public class ShowRepository : IShowRepository
    {
        private readonly RelativeRankContext _context;

        public ShowRepository(RelativeRankContext context) => _context = context;

        public async Task<bool> AddShow(RankedShow show)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RankedShow>> GetAllShows()
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

        public Task<bool> RemoveShow(RankedShow show)
        {
            throw new NotImplementedException();
        }
    }
}
