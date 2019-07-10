using RelativeRank.EntityFrameworkEntities;
using System.Collections.Generic;
using RelativeRank.Entities;

namespace RelativeRank.Interfaces
{
    public interface IRelativeRankRepository
    {
        List<RankedShow> GetAllShows();
        bool SignUp(string username, string password);
        string Login(string username, string password);
        void AddShow(Show show, string adminPassword);
        void RemoveShow(Show show, string adminPassword);
        RankedShowList GetUsersShows(string username);
        void UpdateUsersShows(string username, string password, RankedShowList updatedList);
    }
}
