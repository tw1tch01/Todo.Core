using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemPreviouslyCompletedResult : ItemInvalidResult
    {
        private const string _message = "Item previously completed on {0}.";
        private const string _completedOnKey = nameof(TodoItem.CompletedOn);

        public ItemPreviouslyCompletedResult(Guid itemId, DateTime completedOn)
            : base(itemId, GetMessage(completedOn))
        {
            Data[_completedOnKey] = completedOn;
        }

        private static string GetMessage(DateTime completedOn)
        {
            return string.Format(_message, completedOn);
        }
    }
}