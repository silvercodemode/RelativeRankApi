using RelativeRank.Entities;
using System.Threading.Tasks;

namespace RelativeRank.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(string username, string password);
        Task<User> SignUp(string username, string password);
        Task<RankedShowList> GetUsersShows(string username);
        Task<bool> UpdateUsersShows(User user, RankedShowList updatedList);
    }
}
