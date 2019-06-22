using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelativeRank.Data;
using RelativeRank.EntityFrameworkEntities;
using RelativeRank.Interfaces;
using Microsoft.EntityFrameworkCore;
using RelativeRank.Entities;

namespace RelativeRank.Data
{
    public class EfSqlServerRepository : IRelativeRankRepository
    {
        private readonly RelativeRankContext _context;
        public EfSqlServerRepository() =>_context = new RelativeRankContext();
        public void AddShow(Show show)
        {
            throw new NotImplementedException();
        }

        public void AddShowToUsersShows(string username, RankedShow show)
        {
            throw new NotImplementedException();
        }

        public List<Show> GetAllShows()
        {
            var allShows = _context.Show.ToList();
            return allShows;
        }

        public List<RankedShow> GetUsersShows(string username)
        {
            throw new NotImplementedException();
        }

        public void RemoveShow(Show show)
        {
            throw new NotImplementedException();
        }

        public void RemoveUsersShow(string username, RankedShow show)
        {
            throw new NotImplementedException();
        }

        public void UpdateUsersShowRank(string username, RankedShow show, short newRank)
        {
            throw new NotImplementedException();
        }
    }
}
