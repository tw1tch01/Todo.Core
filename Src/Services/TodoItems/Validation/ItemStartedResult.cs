using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemStartedResult : ItemValidResult
    {
        private const string _message = "Item successfully started.";
        private const string _startedOnKey = nameof(TodoItem.StartedOn);

        public ItemStartedResult(Guid itemId, DateTime startedOn)
            : base(itemId, _message)
        {
            Data[_startedOnKey] = startedOn;
        }
    }
}