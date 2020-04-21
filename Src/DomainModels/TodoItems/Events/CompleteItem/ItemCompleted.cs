using System;

namespace Todo.DomainModels.TodoItems.Events.CompleteItem
{
    public class ItemCompleted
    {
        public ItemCompleted(Guid itemId, DateTime cancelledOn)
        {
            ItemId = itemId;
            CancelledOn = cancelledOn;
        }

        public Guid ItemId { get; }
        public DateTime CancelledOn { get; }
    }
}