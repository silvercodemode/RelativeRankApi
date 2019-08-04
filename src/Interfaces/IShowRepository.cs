using RelativeRank.EntityFrameworkEntities;
using System.Collections.Generic;
using RelativeRank.Entities;
using System.Threading.Tasks;

namespace RelativeRank.Interfaces
{
    public interface IShowRepository
    {
        Task<List<RankedShow>> GetAllShows();
        Task<bool> AddShow(RankedShow show);
        Task<bool> RemoveShow(RankedShow show);
    }
}
