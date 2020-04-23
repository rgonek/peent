using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Peent.Application.Specifications
{
    public abstract class BaseSpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }

        private readonly List<Expression<Func<T, object>>> _includes = new List<Expression<Func<T, object>>>();
        public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes.AsReadOnly();
        private readonly List<string> _includeStrings = new List<string>();
        public IReadOnlyList<string> IncludeStrings => _includeStrings.AsReadOnly();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
            => _includes.Add(includeExpression);

        protected void AddInclude(string includeString)
            => _includeStrings.Add(includeString);

        protected void SetCriteria(Expression<Func<T, bool>> criteria)
            => Criteria = criteria;
    }
}
