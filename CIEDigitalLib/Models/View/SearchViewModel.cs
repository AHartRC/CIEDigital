using System.Collections.Generic;
using CIEDigitalLib.Search;

namespace CIEDigitalLib.Models.View
{
    public class SearchViewModel<T>
    {
        public IEnumerable<T> Data { get; set; }

        public IEnumerable<AbstractSearch> SearchCriteria { get; set; }
    }
}