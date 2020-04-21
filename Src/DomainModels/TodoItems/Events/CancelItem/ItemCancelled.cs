using System;

namespace Todo.DomainModels.TodoItems.Events.CancelItem
{
    public class ItemCancelled
    {
        public ItemCancelled(Guid itemId, DateTime cancelledOn)
        {
            ItemId = itemId;
            CancelledOn = cancelledOn;
        }

        public Guid ItemId { get; }

        public DateTime CancelledOn { get; }
    }
}