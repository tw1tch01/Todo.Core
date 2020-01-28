using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.TodoItems.Specifications
{
    internal class GetItemsByParentId : LinqSpecification<TodoItem>
    {
        private readonly Guid _parentId;

        public GetItemsByParentId(Guid parentId)
        {
            _parentId = parentId;
        }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => item.ParentItemId == _parentId;
        }
    }
}