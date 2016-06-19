using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CIEDigitalLib.Enumerators;
using CIEDigitalLib.Models.Result;
using CIEDigitalLib.Models.View;
using Microsoft.Ajax.Utilities;

namespace CIEDigitalLib.Helpers
{
    public static class PagingHtmlHelper
    {
        public static string GetSortingUrl<T>(this HtmlHelper html, PagedResult<T> pagedResult, string propertyName, string url)
        {
            var extendedUrl = url
                .SetParameter("sortColumn", propertyName)
                .SetParameter("sortDirection", GetSortDirection(pagedResult.Paging, propertyName).ToString())
                .SetParameter("pageIndex", "0");

            return extendedUrl;
        }
        public static string GetSortingUrl<T, TProperty>(this HtmlHelper html, PagedResult<T> pagedResult, Expression<Func<T, TProperty>> property, string url = "")
        {
            if (pagedResult == null || property == null)
            {
                return null;
            }

            var name = property.GetCorrectPropertyName();
            return GetSortingUrl(html, pagedResult, name, url.Trim().IfNullOrWhiteSpace(HttpContext.Current.Request.RawUrl));
        }
        public static string GetCorrectPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }

            var op = ((UnaryExpression)expression.Body).Operand;
            return ((MemberExpression)op).Member.Name;
        }

        public static MvcHtmlString GetPager<T>(this HtmlHelper html, PagedResult<T> pagedResult, string url)
        {
            if (pagedResult == null || pagedResult.TotalNumberOfItems <= pagedResult.Paging.PageSize)
            {
                return MvcHtmlString.Create(string.Empty);
            }

            var listBuilder = new TagBuilder("ul");
            listBuilder.AddCssClass("pagination");

            var pagingIndexes = GetPagingIndexes(
                pagedResult.Paging.PageIndex,
                (int)Math.Ceiling((double)pagedResult.TotalNumberOfItems / pagedResult.Paging.PageSize));

            for (var i = 0; i < pagingIndexes.Length; i++)
            {
                if (i > 0 && pagingIndexes[i - 1] != pagingIndexes[i] - 1)
                {
                    var extraLiBuilder = new TagBuilder("li") { InnerHtml = "<span>&hellip;</span>" };
                    extraLiBuilder.AddCssClass("disabled");
                    listBuilder.InnerHtml += extraLiBuilder.ToString();
                }

                var itemBuilder = new TagBuilder("li");
                if (pagedResult.Paging.PageIndex == pagingIndexes[i])
                {
                    itemBuilder.InnerHtml = "<span>" + (pagingIndexes[i] + 1) + "</span>";
                    itemBuilder.AddCssClass("active");
                }
                else
                {
                    var pagingLinkBuilder = new TagBuilder("a");

                    var extendedUrl = url
                        .SetParameter("sortColumn", pagedResult.Paging.SortColumn)
                        .SetParameter("sortDirection", pagedResult.Paging.SortDirection.ToString())
                        .SetParameter("pageIndex", pagingIndexes[i].ToString());

                    pagingLinkBuilder.MergeAttribute("href", extendedUrl);
                    pagingLinkBuilder.AddCssClass("paging");
                    pagingLinkBuilder.SetInnerText((pagingIndexes[i] + 1).ToString());

                    itemBuilder.InnerHtml = pagingLinkBuilder.ToString();
                }

                listBuilder.InnerHtml += itemBuilder.ToString();
            }

            return MvcHtmlString.Create(listBuilder.ToString());
        }

        /// <summary>
        ///     Replaces a parameter within an URL.
        ///     If <c>null</c> is supplied as new value, the parameter gets removed.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The new URL.</returns>
        private static string SetParameter(this string url, string param, string value)
        {
            var questionMarkIndex = url.IndexOf('?');
            NameValueCollection parameters;
            var result = new StringBuilder();

            if (questionMarkIndex == -1)
            {
                parameters = new NameValueCollection();
                result.Append(url);
            }
            else
            {
                parameters = HttpUtility.ParseQueryString(url.Substring(questionMarkIndex));
                result.Append(url.Substring(0, questionMarkIndex));
            }

            if (string.IsNullOrEmpty(value))
            {
                parameters.Remove(param);
            }
            else
            {
                parameters[param] = value;
            }

            if (parameters.Count > 0)
            {
                result.Append('?');

                foreach (string parameterName in parameters)
                {
                    result.AppendFormat("{0}={1}&", parameterName, parameters[parameterName]);
                }

                result.Remove(result.Length - 1, 1);
            }

            return result.ToString();
        }

        private static SortDirection GetSortDirection(Paging paging, string propertyName)
        {
            var sortDirection = SortDirection.Ascending;

            if (paging != null
                && propertyName.Equals(paging.SortColumn)
                && paging.SortDirection == SortDirection.Ascending)
            {
                sortDirection = SortDirection.Descending;
            }

            return sortDirection;
        }

        private static int[] GetPagingIndexes(int currentIndex, int totalPages)
        {
            var result = new HashSet<int>();

            for (var i = 0; i < 2; i++)
            {
                if (i <= totalPages)
                {
                    result.Add(i);
                }
            }

            var current = currentIndex - 3;

            while (current <= currentIndex + 3)
            {
                if (current > 0 && current < totalPages)
                {
                    result.Add(current);
                }

                current++;
            }

            for (var i = totalPages - 2; i < totalPages; i++)
            {
                result.Add(i);
            }

            return result.ToArray();
        }
    }
}