using Irony.Parsing;

namespace CIEDigitalLib.Search.Irony
{
    [Language("Search Grammar", "1.0", "A search grammar")]
    public class SearchGrammar : Grammar
    {
        public SearchGrammar()
            : base(false)
        {
            // Terminals
            var term = new IdentifierTerminal(
                "Term",
                "!@#$%^*_'.?",
                "!@#$%^*_'.?0123456789üöäÜÖÄß");

            term.Priority = TerminalPriority.Low;
            var phrase = new StringLiteral("Phrase", "\"");

            // Keywords
            var and = ToTerm("AND");
            var or = ToTerm("OR");
            var not = ToTerm("NOT");

            // Set precedence of operators.
            RegisterOperators(1, not);
            RegisterOperators(2, and);
            RegisterOperators(3, or);

            // NonTerminals
            var orExpression = new NonTerminal("OrExpression");
            var andExpression = new NonTerminal("AndExpression");
            var andOperator = new NonTerminal("AndOperator");
            var orOperator = new NonTerminal("OrOperator");
            var notOperator = new NonTerminal("NotOperator");
            var negatedExpression = new NonTerminal("NegatedExpression");
            var primaryExpression = new NonTerminal("PrimaryExpression");
            var parenthesizedExpression = new NonTerminal("ParenthesizedExpression");

            Root = orExpression;

            orExpression.Rule = andExpression
                                | orExpression + orOperator + andExpression;

            andExpression.Rule = primaryExpression
                                 | andExpression + andOperator + primaryExpression;

            andOperator.Rule = Empty
                               | and
                               | "&";

            orOperator.Rule = or
                              | "|";

            notOperator.Rule = not
                               | "-";

            negatedExpression.Rule = notOperator + primaryExpression;

            primaryExpression.Rule = term
                                     | negatedExpression
                                     | parenthesizedExpression
                                     | phrase;

            parenthesizedExpression.Rule = "(" + orExpression + ")";

            MarkPunctuation("(", ")");
        }
    }
}