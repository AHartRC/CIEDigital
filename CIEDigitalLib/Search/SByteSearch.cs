using System;
using System.Linq.Expressions;
using CIEDigitalLib.Enumerators;

namespace CIEDigitalLib.Search
{
    public class SByteSearch : AbstractSearch
    {
        public sbyte? SearchTerm { get; set; }
        public sbyte? SearchTerm2 { get; set; }

        public NumericComparators Comparator { get; set; }

        protected override Expression BuildFilterExpression(Expression property)
        {
            Expression searchExpression1 = null;
            Expression searchExpression2 = null;

            if (SearchTerm.HasValue)
            {
                searchExpression1 = GetFilterExpression(property);
            }

            if (SearchTerm2.HasValue)
            {
                switch (Comparator)
                {
                    case NumericComparators.GreaterOrEqual:
                    case NumericComparators.LessOrEqual:
                    case NumericComparators.Within:
                        searchExpression2 = Expression.LessThanOrEqual(property, Expression.Constant(SearchTerm.Value));
                        break;
                    case NumericComparators.Greater:
                    case NumericComparators.Less:
                    case NumericComparators.InRange:
                        searchExpression2 = Expression.LessThan(property, Expression.Constant(SearchTerm.Value));
                        break;
                    case NumericComparators.Equal:
                        searchExpression2 = Expression.Equal(property, Expression.Constant(SearchTerm.Value));
                        break;
                    case NumericComparators.NotEqual:
                        searchExpression2 = Expression.NotEqual(property, Expression.Constant(SearchTerm));
                        break;
                    default:
                        throw new InvalidOperationException("Comparator not supported.");
                }
            }

            var result = searchExpression1 == null && searchExpression2 == null
                ? null
                : (searchExpression1 == null || searchExpression2 == null
                    ? (searchExpression1 ?? searchExpression2)
                    : Expression.AndAlso(searchExpression1, searchExpression2));

            return result;
        }

        private Expression GetFilterExpression(Expression property)
        {
            switch (Comparator)
            {
                case NumericComparators.Less:
                    return Expression.LessThan(property, Expression.Constant(SearchTerm.Value));
                case NumericComparators.LessOrEqual:
                    return Expression.LessThanOrEqual(property, Expression.Constant(SearchTerm.Value));
                case NumericComparators.Equal:
                    return Expression.Equal(property, Expression.Constant(SearchTerm.Value));
                case NumericComparators.GreaterOrEqual:
                case NumericComparators.Within:
                    return Expression.GreaterThanOrEqual(property, Expression.Constant(SearchTerm.Value));
                case NumericComparators.Greater:
                case NumericComparators.InRange:
                    return Expression.GreaterThan(property, Expression.Constant(SearchTerm.Value));
                default:
                    throw new InvalidOperationException("Comparator not supported.");
            }
        }
    }
}