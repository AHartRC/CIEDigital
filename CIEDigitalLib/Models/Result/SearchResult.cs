using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CIEDigitalLib.Models.Result
{
    public class SearchResult<T> : IQueryable<T>
    {
        private readonly IQueryable<T> query;

        public SearchResult(IQueryable<T> query, IEnumerable<string> terms)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (terms == null)
            {
                throw new ArgumentNullException("terms");
            }

            this.query = query;
            Terms = terms;
        }

        public IEnumerable<string> Terms { get; private set; }

        public Type ElementType
        {
            get { return query.ElementType; }
        }

        public Expression Expression
        {
            get { return query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return query.Provider; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return query.GetEnumerator();
        }
    }
}