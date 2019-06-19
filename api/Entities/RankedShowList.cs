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

        public RankedShow this[int i]
        {
            get { return _backingList[i]; }
            set { _backingList[i] = value; }
        }
        public short ShowsInList { get { return (short) _backingList.Count; } }

        public void Add(RankedShow show)
        {
            if (show.Rank <= 0)
            {
                show.Rank = (short)(_backingList.Count + 1);
                _backingList.Add(show);
            }
            else
            {
                _backingList.Add(show);
                var index = _backingList.Count - 2;
                while (index >= 0 && _backingList[index].Rank >= show.Rank)
                {
                    _backingList[index + 1] = _backingList[index];
                    _backingList[index + 1].Rank++;
                    index--;
                }

                _backingList[index + 1] = show;
            }

        }
    }
}
