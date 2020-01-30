using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Services.TodoItems.Specifications
{
    internal class FilterItemsByImportance : LinqSpecification<TodoItem>
    {
        private readonly ImportanceLevel _importance;

        public FilterItemsByImportance(ImportanceLevel importance)
        {
            _importance = importance;
        }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => item.ImportanceLevel == _importance;
        }
    }
}