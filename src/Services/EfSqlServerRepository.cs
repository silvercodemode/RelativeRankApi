using System;
using System.Collections.Generic;
using System.Linq;
using RelativeRank.Interfaces;
using RelativeRank.Entities;

namespace RelativeRank.Services
{
    public class EfSqlServerRepository : IShowRepository
    {
        private readonly RelativeRankContext _context;

        public EfSqlServerRepository(RelativeRankContext context) => _context = context;

        public void AddShow(RankedShow show)
        {
            throw new NotImplementedException();
        }

        public List<RankedShow> GetAllShows()
        {
            var allShows = _context.Show.ToList();
            return allShows.Select(show => new RankedShow() { Name = show.Name }).ToList();
        }

        public void RemoveShow(RankedShow show)
        {
            throw new NotImplementedException();
        }
    }
}
