using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CIEDigitalLib.Search;

namespace CIEDigitalLib.Extensions
{
    public static class SearchExtensions
    {
        public static IQueryable<T> ApplySearchCriteria<T>(this IQueryable<T> query,
            IEnumerable<AbstractSearch> searchCriterias)
        {
            return searchCriterias.Aggregate(query, (current, criteria) => criteria.ApplyToQuery(current));
        }

        public static ICollection<AbstractSearch> GetDefaultSearchCriteria(this Type type, bool sortAlpha = false)
        {
            var properties = type.GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .OrderBy(p => p.PropertyType.IsCollectionType())
                .ThenBy(p => sortAlpha ? p.Name : null);

            var searchCriterias = properties
                .Select(p => CreateSearchCriterion(type, p.PropertyType, p.Name))
                .Where(s => s != null)
                .ToList();

            return searchCriterias;
        }

        public static ICollection<AbstractSearch> AddCustomSearchCriterion<T>(
            this ICollection<AbstractSearch> searchCriterias, Expression<Func<T, object>> property)
        {
            Type propertyType = null;
            var fullPropertyPath = GetPropertyPath(property, out propertyType);

            var searchCriteria = CreateSearchCriterion(typeof (T), propertyType, fullPropertyPath);

            if (searchCriteria != null)
            {
                searchCriterias.Add(searchCriteria);
            }

            return searchCriterias;
        }

        private static AbstractSearch CreateSearchCriterion(Type targetType, Type propertyType, string property)
        {
            AbstractSearch result = null;

            if (propertyType.IsCollectionType())
            {
                propertyType = propertyType.GetGenericArguments().First();
            }

            if (propertyType.IsEnum)
            {
                result = new EnumSearch(propertyType);
            }
            else if (propertyType == typeof(string))
            {
                result = new TextSearch();
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                result = new BooleanSearch();
            }
            else if (propertyType == typeof(byte) || propertyType == typeof(byte?))
            {
                result = new ByteSearch();
            }
            else if (propertyType == typeof(char) || propertyType == typeof(char?))
            {
                result = new CharacterSearch();
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                result = new DateSearch();
            }
            else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
            {
                result = new DecimalSearch();
            }
            else if (propertyType == typeof(double) || propertyType == typeof(double?))
            {
                result = new DoubleSearch();
            }
            else if (propertyType == typeof(float) || propertyType == typeof(float?))
            {
                result = new FloatSearch();
            }
            else if (propertyType == typeof(int) || propertyType == typeof(int?))
            {
                result = new IntegerSearch();
            }
            else if (propertyType == typeof(long) || propertyType == typeof(long?))
            {
                result = new LongSearch();
            }
            else if (propertyType == typeof(sbyte) || propertyType == typeof(sbyte?))
            {
                result = new SByteSearch();
            }
            else if (propertyType == typeof(short) || propertyType == typeof(short?))
            {
                result = new ShortSearch();
            }
            else if (propertyType == typeof(uint) || propertyType == typeof(uint?))
            {
                result = new UnsignedIntegerSearch();
            }
            else if (propertyType == typeof(ulong) || propertyType == typeof(ulong?))
            {
                result = new UnsignedLongSearch();
            }
            else if (propertyType == typeof(ushort) || propertyType == typeof(ushort?))
            {
                result = new UnsignedShortSearch();
            }

            if (result != null)
            {
                result.Property = property;
                result.TargetTypeName = targetType.AssemblyQualifiedName;
            }

            return result;
        }

        private static string GetPropertyPath<T>(Expression<Func<T, object>> expression, out Type targetType)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;

            if (methodCallExpression != null)
            {
                if (methodCallExpression.Arguments.Count == 2)
                {
                    var memberExpression1 = methodCallExpression.Arguments[0] as MemberExpression;
                    var lambdaExpression = methodCallExpression.Arguments[1] as LambdaExpression;

                    if (memberExpression1 != null && lambdaExpression != null)
                    {
                        var memberExpression2 = lambdaExpression.Body as MemberExpression;

                        if (memberExpression2 != null)
                        {
                            targetType = memberExpression2.Type;

                            return string.Format(
                                "{0}.{1}",
                                GetPropertyPath(memberExpression1),
                                GetPropertyPath(memberExpression2));
                        }
                    }
                }

                throw new ArgumentException(
                    "Please provide a lambda expression like 'n => n.Collection.Select(c => c.PropertyName)'",
                    "expression");
            }
            var unaryExpression = expression.Body as UnaryExpression;
            MemberExpression memberExpression = null;

            if (unaryExpression != null)
            {
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = expression.Body as MemberExpression;
            }

            if (memberExpression != null)
            {
                targetType = memberExpression.Type;

                return GetPropertyPath(memberExpression);
            }

            throw new ArgumentException("Please provide a lambda expression like 'n => n.PropertyName'", "expression");
        }

        private static string GetPropertyPath(MemberExpression memberExpression)
        {
            var property = memberExpression.ToString();
            return property.Substring(property.IndexOf('.') + 1);
        }
    }
}