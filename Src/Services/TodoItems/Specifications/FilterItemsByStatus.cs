using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Services.TodoItems.Specifications
{
    public class FilterItemsByStatus : LinqSpecification<TodoItem>
    {
        private readonly TodoItemStatus _status;

        public FilterItemsByStatus(TodoItemStatus status)
        {
            _status = status;
        }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => item.GetStatus().ToString().Equals(_status.ToString());
        }
    }
}