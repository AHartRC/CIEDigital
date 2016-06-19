using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CIEDigitalLib.Models.Context;
using Microsoft.VisualBasic.FileIO;

namespace CIEDigitalLib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Returns a boolean as to whether or not the source string is null, empty, or contains only whitespace characters
        /// </summary>
        /// <param name="source">The source string</param>
        /// <returns>True if the source string is null, empty, or contains only whitespace characters, otherwise false.</returns>
        public static bool IsNullOrWhitespace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        ///     Returns a replacement string if the source string is null, empty, or contains nothing but whtiespace characters
        /// </summary>
        /// <param name="source">The source string to check against</param>
        /// <param name="replacement">The replacement string should the check come back true</param>
        /// <returns>The replacement string if true, otherwise the source string</returns>
        public static string IfNullOrWhitespace(this string source, string replacement)
        {
            if (string.IsNullOrWhiteSpace(source))
                return replacement;

            return source;
        }

        /// <summary>
        ///     Returns a replacement string if the source string is equal to any of the specified strings
        /// </summary>
        /// <param name="source">The source string to check against</param>
        /// <param name="replacement">The replacement string should the check come back true</param>
        /// <param name="input">The values to compare the source string against</param>
        /// <returns>The replacement string if the source string equals any of the input strings, otherwise the source string</returns>
        public static string IfEquals(this string source, string[] input, string replacement)
        {
            if (input.Any(a => a.Equals(source)))
                return replacement;

            return source;
        }

        /// <summary>
        ///     Converts a string into a nullable integer
        /// </summary>
        /// <param name="source">The source string containing the number</param>
        /// <returns>An integer representing the source string</returns>
        public static int? ToNullableInt(this string source)
        {
            int? result;
            if (string.IsNullOrWhiteSpace(source))
            {
                result = null;
            }
            else
            {
                int j;
                if (int.TryParse(source, out j))
                    result = j;
                else
                    result = null;
            }

            return result;
        }

        /// <summary>
        ///     Converts a string decimalo a nullable decimaleger
        /// </summary>
        /// <param name="source">The source string containing the number</param>
        /// <returns>An decimaleger representing the source string</returns>
        public static decimal? ToNullableDecimal(this string source)
        {
            decimal? result;
            if (string.IsNullOrWhiteSpace(source))
            {
                result = null;
            }
            else
            {
                decimal j;
                if (decimal.TryParse(source, out j))
                    result = j;
                else
                    result = null;
            }

            return result;
        }

        public static IEnumerable<string> SplitCSV(this string source)
        {
            using (
                var parser = new TextFieldParser(new StringReader(source))
                {
                    HasFieldsEnclosedInQuotes = true,
                    TextFieldType = FieldType.Delimited,
                    Delimiters = new[] { "," },
                    TrimWhiteSpace = true
                })
            {
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();

                    if (fields == null || !fields.Any())
                        continue;

                    foreach (var field in fields)
                        yield return field;
                }

                parser.Close();
            }
        }

        public static string CleanResponseString(this string source)
        {
            if (source.IsNullOrWhitespace())
                return source;

            source = source.Trim();

            while (Regex.IsMatch(source, @"<head(.*?|[\s\r\n]*?)*?</head>"))
                source = Regex.Replace(source, @"<head(.*?|[\s\r\n]*?)*?</head>", "");

            while (Regex.IsMatch(source, @"<script(.*?|[\s\r\n]*?)*?</script>"))
                source = Regex.Replace(source, @"<script(.*?|[\s\r\n]*?)*?</script>", "");

            while (Regex.IsMatch(source, @"<style(.*?|[\s\r\n]*?)*?</style>"))
                source = Regex.Replace(source, @"<style(.*?|[\s\r\n]*?)*?</style>", "");

            while (Regex.IsMatch(source, @"<form(.*?|[\s\r\n]*?)*?</form>"))
                source = Regex.Replace(source, @"<form(.*?|[\s\r\n]*?)*?</form>", "");

            while (Regex.IsMatch(source, @"<ul(.*?|[\s\r\n]*?)*?</ul>"))
                source = Regex.Replace(source, @"<ul(.*?|[\s\r\n]*?)*?</ul>", "");

            while (Regex.IsMatch(source, @"<ol(.*?|[\s\r\n]*?)*?</ol>"))
                source = Regex.Replace(source, @"<ol(.*?|[\s\r\n]*?)*?</ol>", "");

            while (Regex.IsMatch(source, @"<!--(.*?|[\s\r\n]*?)*?-->"))
                source = Regex.Replace(source, @"<!--(.*?|[\s\r\n]*?)*?-->", "");

            while (Regex.IsMatch(source, @"\n\s*?\n"))
                source = Regex.Replace(source, @"\n\s*?\n", "\n");

            while (source.Contains("\t"))
                source = source.Replace("\t", "");

            while (source.Contains("  "))
                source = source.Replace("  ", "");

            while (source.Contains("\r\n\r\n"))
                source = source.Replace("\r\n\r\n", "\r\n");

            while (Regex.IsMatch(source, @" \n"))
                source = Regex.Replace(source, @" \n", "\n");

            while (Regex.IsMatch(source, @"\n "))
                source = Regex.Replace(source, @"\n ", "\n");

            while (Regex.IsMatch(source, @" \r"))
                source = Regex.Replace(source, @" \r", "\r");

            while (Regex.IsMatch(source, @"\r "))
                source = Regex.Replace(source, @"\r ", "\r");

            source = source.Trim();

            return source;
        }

        public static int GetPositionID(this string name)
        {
            var db = new CIEDigitalEntities();

            if (db.PlayerPositions == null ||
                !db.PlayerPositions.Any(a => a.ShortName.Equals(name) || a.Name.Equals(name)))
                throw new ArgumentNullException("", "There are no player positions in the database");

            if (name.IsNullOrWhitespace())
                throw new ArgumentNullException("name", "An invalid entry was detected for the name of the position.");

            if (db.PlayerPositions.Any(a => a.ShortName.Equals(name)))
                return db.PlayerPositions.First(f => f.ShortName.Equals(name)).ID;

            if (db.PlayerPositions.Any(a => a.Name.Equals(name)))
                return db.PlayerPositions.First(f => f.Name.Equals(name)).ID;

            throw new ArgumentNullException("name", string.Format("No player position matches {0}", name));
        }

        public static int? TryGetPositionID(this string name)
        {
            var db = new CIEDigitalEntities();

            if (db.PlayerPositions == null ||
                !db.PlayerPositions.Any(a => a.ShortName.Equals(name) || a.Name.Equals(name)))
                throw new ArgumentNullException("", "There are no player positions in the database");

            if (name.IsNullOrWhitespace())
                throw new ArgumentNullException("name", "An invalid entry was detected for the name of the position.");

            if (db.PlayerPositions.Any(a => a.ShortName.Equals(name)))
                return db.PlayerPositions.First(f => f.ShortName.Equals(name)).ID;

            if (db.PlayerPositions.Any(a => a.Name.Equals(name)))
                return db.PlayerPositions.First(f => f.Name.Equals(name)).ID;

            return null;
        }
    }
}