using System.Collections.Generic;
using CIEDigitalLib.Enumerators;

namespace CIEDigitalLib.Models.View
{
    public class GrammarSearchViewModel<T>
    {
        public IEnumerable<T> Data { get; set; }

        public string SearchTerm { get; set; }

        public GrammarSearchType Grammar { get; set; }

        public IEnumerable<string> Terms { get; set; }
    }
}