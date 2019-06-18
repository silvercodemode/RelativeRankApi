using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelativeRank.Models;

namespace RelativeRank.Entities
{
    public class RankedShowList
    {
        private List<RankedShow> _backingList;

        public RankedShowList()
        {
            _backingList = new List<RankedShow>();
        }
        public short ShowsInList { get { return (short) _backingList.Count; } }

        public void Add(RankedShow show)
        {
            _backingList.Add(show);
        }
    }
}
