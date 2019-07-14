using RelativeRank.Entities;

namespace RelativeRank.Interfaces
{
    public interface IUserRepository
    {
        User Login(string username, string password);
        User SignUp(string username, string password);
        RankedShowList GetUsersShows(string username);
        void UpdateUsersShows(string username, RankedShowList updatedList);
    }
}
