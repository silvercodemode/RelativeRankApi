using RelativeRank.DataTransferObjects;
using RelativeRank.Entities;
using System.Threading.Tasks;

namespace RelativeRank.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateNewUser(RelativeRank.EntityFrameworkEntities.User newUser);
        Task<RelativeRank.EntityFrameworkEntities.User> GetUserByUsername(string username);
        Task<User> DeleteUser(User userToDelete);
    }
}
