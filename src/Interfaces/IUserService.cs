using RelativeRank.Entities;
using RelativeRank.DataTransferObjects;
using System.Threading.Tasks;

namespace RelativeRank.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> CreateNewUser(SignUpModel newUser);
        Task<User?> Authenticate(LoginModel userAuthentication);
        RankedShowList GetUsersShowList(int userId);
        Task<RankedShowList> UpdateUsersShowList(int userId, RankedShowList rankedShowList);
        Task<User?> DeleteUser(DeleteUserModel userToDelete);
    }
}
