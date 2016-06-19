using System.Collections.Generic;
using System.Linq.Expressions;

namespace CIEDigitalLib.Models.Result
{
    public class GrammarResult
    {
        public GrammarResult(Expression expression, IEnumerable<string> terms)
        {
            Expression = expression;
            Terms = terms;
        }

        public Expression Expression { get; private set; }

        public IEnumerable<string> Terms { get; private set; }
    }
}