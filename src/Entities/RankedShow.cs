using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.Entities
{
    public class RankedShow
    {
        public string Name { get; set; }
        public short Rank { get; set; }
        public double PercentileRank{ get; set; }
    }
}
