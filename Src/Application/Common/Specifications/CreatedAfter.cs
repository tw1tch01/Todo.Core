using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Common;

namespace Todo.Application.Common.Specifications
{
    public class CreatedAfter<T> : LinqSpecification<T> where T : class, ICreatedAudit
    {
        private readonly DateTime _createdAfter;

        public CreatedAfter(DateTime createdAfter)
        {
            _createdAfter = createdAfter;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            return a => a.CreatedOn > _createdAfter;
        }
    }
}