using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Specifications
{
    public class GetParentItems : LinqSpecification<TodoItem>
    {
        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => !item.ParentItemId.HasValue;
        }
    }
}