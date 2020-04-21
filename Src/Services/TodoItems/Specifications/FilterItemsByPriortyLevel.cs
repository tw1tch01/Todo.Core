using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Services.TodoItems.Specifications
{
    public class FilterItemsByPriortyLevel : LinqSpecification<TodoItem>
    {
        private readonly PriorityLevel _priority;

        public FilterItemsByPriortyLevel(PriorityLevel priority)
        {
            _priority = priority;
        }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => item.PriorityLevel == _priority;
        }
    }
}