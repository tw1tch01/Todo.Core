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
            switch (_status)
            {
                case TodoItemStatus.Completed:
                    return item => item.CompletedOn.HasValue;

                case TodoItemStatus.Cancelled:
                    return item => item.CancelledOn.HasValue;

                case TodoItemStatus.InProgress:
                    return item => !item.CompletedOn.HasValue &&
                                   !item.CancelledOn.HasValue &&
                                   !(item.DueDate.HasValue && item.DueDate < DateTime.UtcNow) &&
                                   item.StartedOn.HasValue;

                case TodoItemStatus.Overdue:
                    return item => !item.CompletedOn.HasValue &&
                                   !item.CancelledOn.HasValue &&
                                   item.DueDate < DateTime.UtcNow;

                case TodoItemStatus.Pending:
                default:
                    return item => !item.CompletedOn.HasValue &&
                                   !item.CancelledOn.HasValue &&
                                   !(item.DueDate.HasValue && item.DueDate < DateTime.UtcNow) &&
                                   !item.StartedOn.HasValue;
            }
        }
    }
}