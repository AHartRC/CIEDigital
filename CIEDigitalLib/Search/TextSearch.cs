using System.Linq.Expressions;
using CIEDigitalLib.Enumerators;

namespace CIEDigitalLib.Search
{
    public class TextSearch : AbstractSearch
    {
        public string SearchTerm { get; set; }

        public TextComparators Comparator { get; set; }

        protected override Expression BuildFilterExpression(Expression property)
        {
            if (SearchTerm == null)
            {
                return null;
            }

            var searchExpression = Expression.Call(
                property,
                typeof (string).GetMethod(Comparator.ToString(), new[] {typeof (string)}),
                Expression.Constant(SearchTerm));

            return searchExpression;
        }
    }
}