using RelativeRank.Entities;
using System.Threading.Tasks;

namespace RelativeRank.Interfaces
{
    public interface IUserShowListRepository
    {
        RankedShowList GetUsersShowList(int userId);
        Task<RankedShowList> UpdateUsersShowList(int userId, RankedShowList rankedShowList);
    }
}
