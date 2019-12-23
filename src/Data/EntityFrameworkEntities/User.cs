using System.Collections.Generic;

namespace RelativeRank.EntityFrameworkEntities
{
    public partial class User
    {
        public User()
        {
            UserToShowMapping = new HashSet<UserToShowMapping>();
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<UserToShowMapping> UserToShowMapping { get; set; }
    }
}
