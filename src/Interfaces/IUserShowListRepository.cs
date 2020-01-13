using RelativeRank.Entities;
using System.Threading.Tasks;

namespace RelativeRank.Interfaces
{
    public interface IUserShowListRepository
    {
        RankedShowList GetUsersShowList(int userId);
        Task<RankedShowList> SetUsersShowList(int userId, RankedShowList rankedShowList);
        void DeleteUsersShowList(int userId);
    }
}
