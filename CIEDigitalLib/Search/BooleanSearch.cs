using System;
using System.Linq.Expressions;
using CIEDigitalLib.Enumerators;

namespace CIEDigitalLib.Search
{
    public class BooleanSearch : AbstractSearch
    {
        public bool? SearchTerm { get; set; }

        public EqualityComparators Comparator { get; set; }

        protected override Expression BuildFilterExpression(Expression property)
        {
            switch (Comparator)
            {
                case EqualityComparators.Equal:
                    return Expression.Equal(property, Expression.Constant(SearchTerm.Value));
                case EqualityComparators.NotEqual:
                    return Expression.NotEqual(property, Expression.Constant(SearchTerm.Value));
                default:
                    throw new InvalidOperationException("Comparator not supported.");
            }
        }
    }
}