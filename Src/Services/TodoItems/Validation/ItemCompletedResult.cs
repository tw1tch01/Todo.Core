using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemCompletedResult : ItemValidResult
    {
        private const string _message = "Item was successfully completed.";
        private const string _completedOnKey = nameof(TodoItem.CompletedOn);

        public ItemCompletedResult(Guid itemId, DateTime completedOn)
            : base(itemId, _message)
        {
            Data[_completedOnKey] = completedOn;
        }
    }
}