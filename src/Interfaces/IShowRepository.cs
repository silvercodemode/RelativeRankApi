using RelativeRank.EntityFrameworkEntities;
using System.Collections.Generic;
using RelativeRank.Entities;

namespace RelativeRank.Interfaces
{
    public interface IShowRepository
    {
        List<RankedShow> GetAllShows();
        void AddShow(RankedShow show);
        void RemoveShow(RankedShow show);
    }
}
