using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CIEDigitalLib.Helpers;
using CIEDigitalLib.Models.Result;

namespace CIEDigitalLib.Extensions
{
    public static class HtmlExtensions
    {
        private static string Encrypt(string plainText)
        {
            //string key = "jdsg432387#";
            string key = "NF7F00B@77#";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public static MvcHtmlString EncryptedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            string AreaName = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    string elementName = d.Keys.ElementAt(i).ToLower();
                    if (elementName == "area")
                    {
                        AreaName = Convert.ToString(d.Values.ElementAt(i));
                        continue;
                    }
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            //<a href="/Answer?questionId=14">What is Entity Framework??</a>
            StringBuilder ancor = new StringBuilder();
            ancor.Append("<a ");
            if (htmlAttributesString != string.Empty)
            {
                ancor.Append(htmlAttributesString);
            }
            ancor.Append(" href=\"");
            if (AreaName != string.Empty)
            {
                ancor.Append("/" + AreaName);
            }
            if (controllerName != string.Empty)
            {
                ancor.Append("/" + controllerName);
            }

            if (actionName != "Index")
            {
                ancor.Append("/" + actionName);
            }
            if (queryString != string.Empty)
            {
                ancor.Append("?q=" + Encrypt(queryString));
            }
            ancor.Append("\"");
            ancor.Append(">");
            ancor.Append(linkText);
            ancor.Append("</a>");
            return new MvcHtmlString(ancor.ToString());
        }

        public static MvcHtmlString EncryptedSortingLink<T, TProperty>(this HtmlHelper<T> html, PagedResult<T> pagedResult, Expression<Func<T, TProperty>> property, string url = "", object htmlAttributes = null)
        {
            if (pagedResult == null || property == null)
            {
                return null;
            }

            string propertyName = property.GetCorrectPropertyName();
            string text = html.GetDisplayName(property).IfNullOrWhitespace(propertyName);
            string innerHtml = HttpUtility.HtmlEncode(text);
            string targetUrl = html.GetSortingUrl(pagedResult, property, url.IfNullOrWhitespace(HttpContext.Current.Request.RawUrl));

            if (innerHtml.IsNullOrWhitespace() || targetUrl.IsNullOrWhitespace())
            {
                return null;
            }

            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = !string.IsNullOrEmpty(text) ? HttpUtility.HtmlEncode(text) : propertyName
            };

            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), true);
            tagBuilder.MergeAttribute("href", targetUrl);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
        #region Property Attribute Getters
        public static object GetModel<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.Model;
        }

        public static Dictionary<string, object> GetAdditionalValues<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.AdditionalValues;
        }

        public static object GetContainer<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.Container;
        }

        public static Type GetContainerType<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.ContainerType;
        }

        public static bool GetConvertEmptyStringToNull<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.ConvertEmptyStringToNull;
        }

        public static string GetDataTypeName<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.DataTypeName;
        }

        public static string GetDescription<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.Description;
        }

        public static string GetDisplayFormatString<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.DisplayFormatString;
        }

        public static string GetDisplayName<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.DisplayName;
        }

        public static string GetEditFormatString<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.EditFormatString;
        }

        public static bool GetHideSurroundingHtml<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.HideSurroundingHtml;
        }

        public static bool GetHtmlEncode<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.HtmlEncode;
        }

        public static bool GetIsComplexType<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.IsComplexType;
        }

        public static bool GetIsNullableValueType<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.IsNullableValueType;
        }

        public static bool GetIsReadOnly<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.IsReadOnly;
        }

        public static bool GetIsRequired<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.IsRequired;
        }

        public static Type GetModelType<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.ModelType;
        }

        public static string GetNullDisplayText<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.NullDisplayText;
        }

        public static int GetOrder<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(int);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.Order;
        }

        public static IEnumerable<ModelMetadata> GetProperties<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.Properties;
        }

        public static string GetPropertyName<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.PropertyName;
        }

        public static bool GetRequestValidationEnabled<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.RequestValidationEnabled;
        }

        public static string GetShortDisplayName<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.ShortDisplayName;
        }

        public static bool GetShowForDisplay<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.ShowForDisplay;
        }

        public static bool GetShowForEdit<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default(bool);
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.ShowForEdit;
        }

        public static string GetSimpleDisplayText<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.SimpleDisplayText;
        }

        public static string GetTemplateHint<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.TemplateHint;
        }

        public static string GetWatermark<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.Watermark;
        }
        #endregion Property Attribute Getters
    }
}
