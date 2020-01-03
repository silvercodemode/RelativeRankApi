using RelativeRank.Entities;
using System.Collections.Generic;

namespace RelativeRank.DataTransferObjects
{
    public class UpdateUserShowListModel
    {
        public string Username { get; set; }
        public List<RankedShow> ShowList { get; set; }
    }
}
