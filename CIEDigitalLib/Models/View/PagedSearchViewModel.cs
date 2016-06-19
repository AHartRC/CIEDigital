using System.Collections.Generic;
using CIEDigitalLib.Models.Result;
using CIEDigitalLib.Search;

namespace CIEDigitalLib.Models.View
{
    public class PagedSearchViewModel<T>
    {
        public PagedResult<T> Data { get; set; }

        public IEnumerable<AbstractSearch> SearchCriteria { get; set; }
    }
}