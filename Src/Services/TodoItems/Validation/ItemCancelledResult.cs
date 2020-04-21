using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemCancelledResult : ItemValidResult
    {
        private const string _message = "Item was successfully cancelled.";
        private const string _cancelledOnKey = nameof(TodoItem.CancelledOn);

        public ItemCancelledResult(Guid itemId, DateTime cancelledOn)
            : base(itemId, _message)
        {
            Data[_cancelledOnKey] = cancelledOn;
        }
    }
}