using RelativeRank.Entities;
using System.Collections.Generic;

namespace RelativeRank.Interfaces
{
    interface IUserRepository
    {
        List<User> GetUsers();
    }
}
