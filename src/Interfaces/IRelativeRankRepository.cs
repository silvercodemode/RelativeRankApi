using RelativeRank.EntityFrameworkEntities;
using System.Collections.Generic;
using RelativeRank.Entities;

namespace RelativeRank.Interfaces
{
    public interface IRelativeRankRepository
    {
        List<Show> GetAllShows();
        void AddShow(Show show);
        void RemoveShow(Show show);
        List<RankedShow> GetUsersShows(string username);
        void AddShowToUsersShows(string username, RankedShow show);
        void UpdateUsersShowRank(string username, RankedShow show, short newRank);
        void RemoveUsersShow(string username, RankedShow show);
    }
}
