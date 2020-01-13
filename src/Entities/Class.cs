using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.Entities
{
    public class Show
    {
        private readonly string _name;
        public string Name { get { return _name; } }

        public Show(string showName) => _name = showName;
    }
}
