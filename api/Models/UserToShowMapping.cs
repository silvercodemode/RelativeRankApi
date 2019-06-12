using System;
using System.Collections.Generic;

namespace RelativeRank.Models
{
    public partial class UserToShowMapping
    {
        public string Username { get; set; }
        public string Showname { get; set; }
        public short Rank { get; set; }

        public Show ShownameNavigation { get; set; }
        public User UsernameNavigation { get; set; }
    }
}
