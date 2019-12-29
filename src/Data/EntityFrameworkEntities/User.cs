using System.Collections.Generic;

namespace RelativeRank.EntityFrameworkEntities
{
    public partial class User
    {
        public User()
        {
            UserToShowMapping = new HashSet<UserToShowMapping>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<UserToShowMapping> UserToShowMapping { get; set; }
    }
}
