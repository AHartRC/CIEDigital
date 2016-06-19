//using System.Text;
//using Antlr4.Runtime.Tree;
//using CIEDigitalLib.Search.Antlr.Generated;

//namespace CIEDigitalLib.Search.Antlr
//{
//    internal class StringBuilderVisitor : SearchGrammarBaseVisitor<string>
//    {
//        private int currentDepth;
//        private StringBuilder stringBuilder = new StringBuilder();

//        public override string Visit(IParseTree tree)
//        {
//            stringBuilder = new StringBuilder();

//            base.Visit(tree);

//            return stringBuilder.ToString();
//        }

//        public override string VisitOrExpression(SearchGrammarParser.OrExpressionContext context)
//        {
//            stringBuilder.AppendLine(new string(' ', currentDepth) + "OrExpression");
//            currentDepth += 2;

//            var result = base.VisitOrExpression(context);

//            currentDepth -= 2;

//            return result;
//        }

//        public override string VisitAndExpression(SearchGrammarParser.AndExpressionContext context)
//        {
//            stringBuilder.AppendLine(new string(' ', currentDepth) + "AndExpression");
//            currentDepth += 2;

//            var result = base.VisitAndExpression(context);

//            currentDepth -= 2;

//            return result;
//        }

//        public override string VisitPrimaryExpression(SearchGrammarParser.PrimaryExpressionContext context)
//        {
//            stringBuilder.AppendLine(new string(' ', currentDepth) + "PrimaryExpression");
//            currentDepth += 2;

//            var result = base.VisitPrimaryExpression(context);

//            currentDepth -= 2;

//            return result;
//        }

//        public override string VisitParenthesizedExpression(SearchGrammarParser.ParenthesizedExpressionContext context)
//        {
//            stringBuilder.AppendLine(new string(' ', currentDepth) + "ParenthesizedExpression");
//            currentDepth += 2;

//            var result = base.VisitParenthesizedExpression(context);

//            currentDepth -= 2;

//            return result;
//        }

//        public override string VisitTerminal(ITerminalNode node)
//        {
//            stringBuilder.AppendLine(new string(' ', currentDepth) + "Terminal: '" + node.GetText() + "'");
//            return null;
//        }

//        public override string VisitPhraseExpression(SearchGrammarParser.PhraseExpressionContext context)
//        {
//            stringBuilder.AppendLine(new string(' ', currentDepth) + "PhraseExpression");
//            currentDepth += 2;

//            var result = base.VisitPhraseExpression(context);

//            currentDepth -= 2;

//            return result;
//        }

//        public override string VisitNegatedExpression(SearchGrammarParser.NegatedExpressionContext context)
//        {
//            stringBuilder.AppendLine(new string(' ', currentDepth) + "NegatedExpression" + "'");
//            currentDepth += 2;

//            var result = base.VisitNegatedExpression(context);

//            currentDepth -= 2;

//            return result;
//        }

//        public override string VisitErrorNode(IErrorNode node)
//        {
//            stringBuilder.AppendLine(new string(' ', currentDepth) + "Error: '" + node.GetText());
//            return null;
//        }
//    }
//}