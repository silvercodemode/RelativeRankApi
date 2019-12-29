using System;
using System.Collections.Generic;

namespace RelativeRank.EntityFrameworkEntities
{
    public partial class Show
    {
        public Show()
        {
            UserToShowMapping = new HashSet<UserToShowMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserToShowMapping> UserToShowMapping { get; set; }
    }
}
