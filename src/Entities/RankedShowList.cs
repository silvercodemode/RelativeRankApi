using System.Collections.Generic;

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
        public int ShowsInList { get { return  _backingList.Count; } }

        public void Add(RankedShow show)
        {
            if (show.Rank <= 0 || show.Rank > _backingList.Count)
            {
                show.Rank = (_backingList.Count + 1);
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

            foreach (var showToUpdatePercentileRank in _backingList)
            {
                showToUpdatePercentileRank.PercentileRank = GetPercentileRankAtIndex(showToUpdatePercentileRank.Rank - 1);
            }
        }

        public double GetPercentileRankAtIndex(int index)
        {
            return 1 - (1 / ((double) (_backingList.Count + 1))) * (index + 1);
        }
    }
}
