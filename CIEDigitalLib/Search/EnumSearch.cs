using System;
using System.Linq.Expressions;
using CIEDigitalLib.Enumerators;

namespace CIEDigitalLib.Search
{
    public class EnumSearch : AbstractSearch
    {
        public EnumSearch()
        {
        }

        public EnumSearch(Type enumType)
        {
            EnumTypeName = enumType.AssemblyQualifiedName;
        }

        public string SearchTerm { get; set; }

        public Type EnumType
        {
            get { return Type.GetType(EnumTypeName); }
        }

        public string EnumTypeName { get; set; }

        public EqualityComparators Comparator { get; set; }

        protected override Expression BuildFilterExpression(Expression property)
        {
            if (SearchTerm == null)
            {
                return null;
            }

            var enumValue = Enum.Parse(EnumType, SearchTerm);

            switch (Comparator)
            {
                case EqualityComparators.Equal:
                    return Expression.Equal(property, Expression.Constant(enumValue));
                case EqualityComparators.NotEqual:
                    return Expression.NotEqual(property, Expression.Constant(enumValue));
                default:
                    throw new InvalidOperationException("Comparator not supported.");
            }
        }
    }
}