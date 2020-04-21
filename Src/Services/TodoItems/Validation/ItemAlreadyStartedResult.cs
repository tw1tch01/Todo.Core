using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemAlreadyStartedResult : ItemInvalidResult
    {
        private const string _message = "Item already started on {0}.";
        private const string _startedOnKey = nameof(TodoItem.StartedOn);

        public ItemAlreadyStartedResult(Guid itemId, DateTime startedOn)
            : base(itemId, GetMessage(startedOn))
        {
            Data[_startedOnKey] = startedOn;
        }

        private static string GetMessage(DateTime startedOn)
        {
            return string.Format(_message, startedOn);
        }
    }
}