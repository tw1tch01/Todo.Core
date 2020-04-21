using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemPreviouslyCancelledResult : ItemInvalidResult
    {
        private const string _message = "Item previously cancelled on {0}.";
        private const string _cancelledOnKey = nameof(TodoItem.CancelledOn);

        public ItemPreviouslyCancelledResult(Guid itemId, DateTime cancelledOn)
            : base(itemId, GetMessage(cancelledOn))
        {
            Data[_cancelledOnKey] = cancelledOn;
        }

        private static string GetMessage(DateTime cancelledOn)
        {
            return string.Format(_message, cancelledOn);
        }
    }
}