﻿//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using Antlr4.Runtime.Tree;
//using CIEDigitalLib.Exceptions;
//using CIEDigitalLib.Models.Result;
//using CIEDigitalLib.Search.Antlr.Generated;

//namespace CIEDigitalLib.Search.Antlr
//{
//    internal class ExpressionBuilderVisitor : SearchGrammarBaseVisitor<GrammarResult>
//    {
//        private readonly MemberExpression[] properties;

//        private readonly HashSet<string> terms = new HashSet<string>();

//        private bool trackTerms = true;

//        public ExpressionBuilderVisitor(params MemberExpression[] properties)
//        {
//            if (properties == null)
//            {
//                throw new ArgumentNullException("property");
//            }

//            this.properties = properties;
//        }

//        public override GrammarResult VisitOrExpression(SearchGrammarParser.OrExpressionContext context)
//        {
//            if (context.ChildCount == 1)
//            {
//                return base.VisitOrExpression(context);
//            }
//            var expression = Expression.OrElse(
//                this.Visit(context.children[0]).Expression,
//                this.Visit(context.children[context.ChildCount - 1]).Expression);

//            return new GrammarResult(expression, terms);
//        }

//        public override GrammarResult VisitAndExpression(SearchGrammarParser.AndExpressionContext context)
//        {
//            if (context.ChildCount == 1)
//            {
//                return base.VisitAndExpression(context);
//            }
//            var expression = Expression.AndAlso(
//                this.Visit(context.children[0]).Expression,
//                this.Visit(context.children[context.ChildCount - 1]).Expression);

//            return new GrammarResult(expression, terms);
//        }

//        public override GrammarResult VisitPrimaryExpression(SearchGrammarParser.PrimaryExpressionContext context)
//        {
//            if (context.ChildCount == 0)
//            {
//                throw new InvalidSearchException("Der Suchbegriff ist ungültig:\r\n" + context.Parent.GetText());
//            }

//            return base.VisitPrimaryExpression(context);
//        }

//        public override GrammarResult VisitParenthesizedExpression(
//            SearchGrammarParser.ParenthesizedExpressionContext context)
//        {
//            trackTerms = false;
//            this.Visit(context.children[0]);
//            this.Visit(context.children[2]);
//            trackTerms = true;

//            return this.Visit(context.children[1]);
//        }

//        public override GrammarResult VisitTerminal(ITerminalNode node)
//        {
//            var expression = CreateTextExpression(node.GetText());

//            return new GrammarResult(expression, terms);
//        }

//        public override GrammarResult VisitPhraseExpression(SearchGrammarParser.PhraseExpressionContext context)
//        {
//            trackTerms = false;
//            base.VisitPhraseExpression(context);
//            trackTerms = true;

//            string searchTerm = string.Join(
//                string.Empty,
//                context.children
//                    .Skip(1)
//                    .Take(context.ChildCount - 2)
//                    .Select(c => c.GetText()));

//            var expression = CreateTextExpression(searchTerm);

//            return new GrammarResult(expression, terms);
//        }

//        public override GrammarResult VisitNegatedExpression(SearchGrammarParser.NegatedExpressionContext context)
//        {
//            var childExpression = this.Visit(context.children[context.ChildCount - 1]).Expression;
//            var expression = Expression.Not(childExpression);

//            return new GrammarResult(expression, terms);
//        }

//        public override GrammarResult VisitErrorNode(IErrorNode node)
//        {
//            throw new InvalidSearchException("Der Suchbegriff ist ungültig:\r\n" + node);
//        }

//        private Expression CreateTextExpression(string searchTerm)
//        {
//            if (trackTerms && !string.IsNullOrWhiteSpace(searchTerm))
//            {
//                terms.Add(searchTerm.ToLowerInvariant());
//            }

//            Expression searchExpression = Expression.Constant(false);

//            foreach (var property in properties)
//            {
//                var nullCheckExpression = Expression.NotEqual(property, Expression.Constant(null));

//                var containsExpression = Expression.Call(
//                    property,
//                    typeof (string).GetMethod("Contains", new[] {typeof (string)}),
//                    Expression.Constant(searchTerm));

//                var combinedExpression = Expression.AndAlso(nullCheckExpression, containsExpression);

//                searchExpression = Expression.OrElse(searchExpression, combinedExpression);
//            }

//            return searchExpression;
//        }
//    }
//}