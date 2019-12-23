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
            var allShows = await _context.Show.ToListAsync();
            return allShows.Select(show => new RankedShow() { Name = show.Name }).ToList();
        }

        public Task<bool> RemoveShow(RankedShow show)
        {
            throw new NotImplementedException();
        }
    }
}
