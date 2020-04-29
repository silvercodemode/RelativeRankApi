using RelativeRank.EntityFrameworkEntities;
using System.Collections.Generic;
using RelativeRank.Entities;
using System.Threading.Tasks;
using RelativeRank.DataTransferObjects;

namespace RelativeRank.Interfaces
{
    public interface IShowRepository
    {
        Task<PagedResult<RelativeRankedShow>> GetAllShowsRelativelyRanked(int page, int pageSize);
        IEnumerable<Entities.Show> GetAllShows();
        IEnumerable<Entities.Show> Search(string searchTerm);
        Task<bool> AddShow(AddShowModel show);
        Task<bool> DeleteShow(int showId);
    }
}
