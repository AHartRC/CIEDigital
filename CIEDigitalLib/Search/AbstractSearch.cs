using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CIEDigitalLib.Extensions;

namespace CIEDigitalLib.Search
{
    public abstract class AbstractSearch
    {
        public string Property { get; set; }

        public string TargetTypeName { get; set; }

        public string LabelText
        {
            get
            {
                if (Property == null)
                {
                    return null;
                }

                var arg = Expression.Parameter(Type.GetType(TargetTypeName), "p");
                var propertyInfo = GetPropertyAccess(arg).Member as PropertyInfo;

                if (propertyInfo != null)
                {
                    var displayAttribute = propertyInfo.GetCustomAttributes(true)
                        .OfType<DisplayAttribute>()
                        .Cast<DisplayAttribute>()
                        .FirstOrDefault();

                    if (displayAttribute != null)
                    {
                        return displayAttribute.Name;
                    }
                }

                return Property.Split('.').Last();
            }
        }

        internal IQueryable<T> ApplyToQuery<T>(IQueryable<T> query)
        {
            var parts = Property.Split('.');

            var parameter = Expression.Parameter(typeof(T), "p");

            var filterExpression = BuildFilterExpressionWithNullChecks(null, parameter, null, parts);

            if (filterExpression == null)
            {
                return query;
            }
            var predicate = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
            return query.Where(predicate);
        }

        protected abstract Expression BuildFilterExpression(Expression property);

        private static Expression Combine(Expression first, Expression second)
        {
            return first == null ? second : Expression.AndAlso(first, second);
        }

        private Expression BuildFilterExpressionWithNullChecks(
            Expression filterExpression,
            ParameterExpression parameter,
            Expression property,
            string[] remainingPropertyParts)
        {
            property = Expression.Property(property ?? parameter, remainingPropertyParts[0]);

            BinaryExpression nullCheckExpression;

            if (remainingPropertyParts.Length == 1)
            {
                if (!property.Type.IsValueType || property.Type.IsNullableType())
                {
                    nullCheckExpression = Expression.NotEqual(property, Expression.Constant(null));
                    filterExpression = Combine(filterExpression, nullCheckExpression);
                }

                if (property.Type.IsNullableType())
                {
                    property = Expression.Property(property, "Value");
                }

                Expression searchExpression = null;
                if (property.Type.IsCollectionType())
                {
                    parameter = Expression.Parameter(property.Type.GetGenericArguments().First());

                    searchExpression = ApplySearchExpressionToCollection(
                        parameter,
                        property,
                        BuildFilterExpression(parameter));
                }
                else
                {
                    searchExpression = BuildFilterExpression(property);
                }

                return searchExpression == null
                        ? null
                        : Combine(filterExpression, searchExpression);
            }

            nullCheckExpression = Expression.NotEqual(property, Expression.Constant(null));
            filterExpression = Combine(filterExpression, nullCheckExpression);

            if (property.Type.IsCollectionType())
            {
                parameter = Expression.Parameter(property.Type.GetGenericArguments().First());
                var searchExpression = BuildFilterExpressionWithNullChecks(
                    null,
                    parameter,
                    null,
                    remainingPropertyParts.Skip(1).ToArray());

                if (searchExpression == null)
                {
                    return null;
                }
                searchExpression = ApplySearchExpressionToCollection(
                    parameter,
                    property,
                    searchExpression);

                return Combine(filterExpression, searchExpression);
            }
            return BuildFilterExpressionWithNullChecks(filterExpression, parameter, property,
                remainingPropertyParts.Skip(1).ToArray());
        }

        private static Expression ApplySearchExpressionToCollection(ParameterExpression parameter, Expression property,
            Expression searchExpression)
        {
            if (searchExpression != null)
            {
                var asQueryable = typeof(Queryable).GetMethods()
                    .Where(m => m.Name == "AsQueryable")
                    .Single(m => m.IsGenericMethod)
                    .MakeGenericMethod(property.Type.GetGenericArguments());

                var anyMethod = typeof(Queryable).GetMethods()
                    .Where(m => m.Name == "Any")
                    .Single(m => m.GetParameters().Length == 2)
                    .MakeGenericMethod(property.Type.GetGenericArguments());

                searchExpression = Expression.Call(
                    null,
                    anyMethod,
                    Expression.Call(null, asQueryable, property),
                    Expression.Lambda(searchExpression, parameter));
            }
            return searchExpression;
        }

        private MemberExpression GetPropertyAccess(ParameterExpression arg)
        {
            var parts = Property.Split('.');

            var property = Expression.Property(arg, parts[0]);

            for (var i = 1; i < parts.Length; i++)
            {
                if (property.Type.IsCollectionType())
                {
                    property = Expression.Property(
                        Expression.Parameter(property.Type.GetGenericArguments().First()),
                        parts[i]);
                }
                else
                {
                    property = Expression.Property(property, parts[i]);
                }
            }

            return property;
        }
    }
}