using RelativeRank.EntityFrameworkEntities;
using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.Tests
{
    public class TestRepository : IShowRepository
    {
        public void AddShow(RankedShow show)
        {
            throw new NotImplementedException();
        }

        public List<RankedShow> GetAllShows()
        {
            throw new NotImplementedException();
        }

        public RankedShowList GetUsersShows(string username)
        {
            throw new NotImplementedException();
        }

        public string Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void RemoveShow(RankedShow show)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void UpdateUsersShows(string username, RankedShowList updatedList)
        {
            throw new NotImplementedException();
        }
    }
}
