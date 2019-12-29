using RelativeRank.EntityFrameworkEntities;
using RelativeRank.DataTransferObjects;
using System.Threading.Tasks;

namespace RelativeRank.Interfaces
{
    public interface IUserService
    {
        Task<NewUser> CreateNewUser(NewUser newUser);
        Task<User> Authenticate(UserAuthentication userAuthentication);
    }
}
