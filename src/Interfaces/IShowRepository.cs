using RelativeRank.EntityFrameworkEntities;
using System.Collections.Generic;
using RelativeRank.Entities;
using System.Threading.Tasks;
using RelativeRank.DataTransferObjects;

namespace RelativeRank.Interfaces
{
    public interface IShowRepository
    {
        Task<List<RankedShow>> GetAllShowsRelativelyRanked();
        IEnumerable<Entities.Show> GetAllShows();
        Task<bool> AddShow(AddShowModel show);
        Task<bool> DeleteShow(int showId);
    }
}
