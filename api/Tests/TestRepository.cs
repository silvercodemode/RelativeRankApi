using RelativeRank.Models;
using RelativeRank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.Tests
{
    public class TestRepository : IRelativeRankRepository
    {
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
            throw new NotImplementedException();
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
