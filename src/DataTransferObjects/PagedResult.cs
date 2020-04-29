using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.DataTransferObjects
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int NumberOfPages { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
