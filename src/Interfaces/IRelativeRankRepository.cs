using RelativeRank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
