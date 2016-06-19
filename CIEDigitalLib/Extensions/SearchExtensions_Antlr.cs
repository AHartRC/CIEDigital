//using System;
//using System.Diagnostics;
//using System.Linq;
//using System.Linq.Expressions;
//using CIEDigitalLib.Exceptions;
//using CIEDigitalLib.Models.Result;
//using CIEDigitalLib.Search.Antlr;
//using CIEDigitalLib.Search.Antlr.Generated;

//namespace CIEDigitalLib.Extensions
//{
//    public static class SearchExtensions_Antlr
//    {
//        public static SearchResult<T> FilterUsingAntlr<T>(
//            this IQueryable<T> query,
//            string queryString,
//            params Expression<Func<T, string>>[] properties)
//        {
//            if (query == null)
//            {
//                throw new ArgumentNullException("query");
//            }

//            if (properties == null)
//            {
//                throw new ArgumentNullException("properties");
//            }

//            if (properties.Length == 0)
//            {
//                throw new ArgumentException("At least one property is expected", "properties");
//            }

//            if (string.IsNullOrWhiteSpace(queryString))
//            {
//                return new SearchResult<T>(query, Enumerable.Empty<string>());
//            }

//            var input = new AntlrInputStream(queryString);
//            SearchGrammarLexer lexer = new SearchGrammarLexer(input);
//            var tokens = new CommonTokenStream(lexer);
//            SearchGrammarParser parser = new SearchGrammarParser(tokens);
//            IParseTree parseTree = parser.prog();

//            var arg = Expression.Parameter(typeof (T), "p");
//            var memberExpressions = new MemberExpression[properties.Length];

//            for (var i = 0; i < properties.Length; i++)
//            {
//                var propertyMemberExpression = properties[i].Body as MemberExpression;

//                if (propertyMemberExpression == null)
//                {
//                    throw new ArgumentException(
//                        "The " + i +
//                        "th property is invalid. Please provide property member expression like 'o => o.Name'",
//                        "properties");
//                }

//                var propertyString = propertyMemberExpression.ToString();
//                var name = propertyString.Substring(propertyString.IndexOf('.') + 1);

//                memberExpressions[i] = Expression.Property(arg, name);
//            }

//#if DEBUG
//            var treeText = new StringBuilderVisitor().Visit(parseTree);
//            Trace.WriteLine(treeText);
//#endif

//            GrammarResult searchResult = null;

//            try
//            {
//                var visitor = new ExpressionBuilderVisitor(memberExpressions);
//                searchResult = visitor.Visit(parseTree);
//            }
//            catch (InvalidSearchException)
//            {
//                var failedTreeText = new StringBuilderVisitor().Visit(parseTree);
//                Trace.WriteLine("Failed to parse search grammar '" + queryString + "': \r\n" + failedTreeText);

//                throw;
//            }

//            var searchPredicate = Expression.Lambda<Func<T, bool>>(searchResult.Expression, arg);

//            return new SearchResult<T>(query.Where(searchPredicate), searchResult.Terms);
//        }
//    }
//}