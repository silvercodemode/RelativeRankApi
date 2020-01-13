using System;
using System.Diagnostics.CodeAnalysis;

namespace RelativeRank.Entities
{
    public class RankedShow : IEquatable<RankedShow>
    {
        public string? Name { get; set; }
        public int Rank { get; set; }
        public double PercentileRank{ get; set; }

        public bool Equals([AllowNull] RankedShow other)
        {
            return this.Name == other?.Name;
        }
    }
}
