using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace RelativeRank.Entities
{
    public class RankedShowList : IEnumerable<RankedShow>
    {
        private List<RankedShow> _backingList;

        public RankedShowList()
        {
            _backingList = new List<RankedShow>();
            RankedShows = _backingList;
        }

        public RankedShowList(IEnumerable<RankedShow> shows)
        {
            ReplaceAll(shows);
            RankedShows = _backingList;
        }

        public RankedShow this[int i]
        {
            get { return _backingList[i]; }
            set { _backingList[i] = value; }
        }
        public int NumberOfShowsInList { get { return  _backingList.Count; } }

        public List<RankedShow> RankedShows { get; }

        public void Add(RankedShow show)
        {
            if (show == null)
            {
                throw new ArgumentNullException(nameof(show));
            }

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

        public void ReplaceAll(IEnumerable<RankedShow> shows)
        {
            if (shows == null)
            {
                throw new ArgumentNullException(nameof(shows));
            }

            var usedRanks = new HashSet<int>();
            foreach (var show in shows)
            {
                if (usedRanks.Contains(show.Rank))
                {
                    throw new ArgumentException(
                        new ResourceManager("RelativeRank.Config.ExceptionMessages",
                            Assembly.GetExecutingAssembly())
                                .GetString("NoTiesInRankedList", new CultureInfo("en-US")));
                }
                usedRanks.Add(show.Rank);
            }

            _backingList = new List<RankedShow>(shows);
            _backingList.Sort((a, b) => a.Rank - b.Rank);

            for (var i = 0; i < _backingList.Count; i++)
            {
                _backingList[i].Rank = i + 1;
                _backingList[i].PercentileRank = GetPercentileRankAtIndex(i);
            }
        }

        public double GetPercentileRankAtIndex(int index)
        {
            return 1 - (1 / ((double) (_backingList.Count + 1))) * (index + 1);
        }

        public IEnumerator<RankedShow> GetEnumerator()
        {
            return ((IEnumerable<RankedShow>)_backingList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<RankedShow>)_backingList).GetEnumerator();
        }
    }
}
