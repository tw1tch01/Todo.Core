using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Common;

namespace Todo.Services.Common.Specifications
{
    public class ModifiedBefore<T> : LinqSpecification<T> where T : class, IModifiedAudit
    {
        private readonly DateTime _modifiedBefore;

        public ModifiedBefore(DateTime modifiedBefore)
        {
            _modifiedBefore = modifiedBefore;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            return entity => entity.ModifiedOn <= _modifiedBefore;
        }
    }
}