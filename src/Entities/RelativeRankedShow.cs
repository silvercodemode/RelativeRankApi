using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.Entities
{
    public class RelativeRankedShow
    {
        public String Name { get; set; }
        public double PercentileRank { get; set; }

        public RelativeRankedShow(string name, double percentileRank)
        {
            Name = name;
            PercentileRank = percentileRank;
        }
    }
}
