using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Common;

namespace Todo.Application.Common.Specifications
{
    public class CreatedBefore<T> : LinqSpecification<T> where T : class, ICreatedAudit
    {
        private readonly DateTime _createdBefore;

        public CreatedBefore(DateTime createdBefore)
        {
            _createdBefore = createdBefore;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            return entity => entity.CreatedOn <= _createdBefore;
        }
    }
}