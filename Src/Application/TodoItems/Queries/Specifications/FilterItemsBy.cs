using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;
using Todo.Models.TodoItems.Enums;

namespace Todo.Application.TodoItems.Queries.Specifications
{
    internal class FilterItemsBy
    {
        public class Status : LinqSpecification<TodoItem>
        {
            private readonly FilterTodoItemsBy.Status _status;

            public Status(FilterTodoItemsBy.Status status)
            {
                _status = status;
            }

            public override Expression<Func<TodoItem, bool>> AsExpression()
            {
                return item => item.GetStatus().ToString().Equals(_status.ToString());
            }
        }

        public class ImportanceLevel : LinqSpecification<TodoItem>
        {
            private readonly FilterTodoItemsBy.Importance _importance;

            public ImportanceLevel(FilterTodoItemsBy.Importance importance)
            {
                _importance = importance;
            }

            public override Expression<Func<TodoItem, bool>> AsExpression()
            {
                return item => item.ImportanceLevel.ToString().Equals(_importance.ToString());
            }
        }

        public class PriortyLevel : LinqSpecification<TodoItem>
        {
            private readonly FilterTodoItemsBy.Priority _priority;

            public PriortyLevel(FilterTodoItemsBy.Priority priority)
            {
                _priority = priority;
            }

            public override Expression<Func<TodoItem, bool>> AsExpression()
            {
                return item => item.PriorityLevel.ToString().Equals(_priority.ToString());
            }
        }
    }
}