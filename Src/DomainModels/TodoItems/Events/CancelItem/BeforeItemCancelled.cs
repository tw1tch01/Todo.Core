using System;

namespace Todo.DomainModels.TodoItems.Events.CancelItem
{
    public class BeforeItemCancelled
    {
        public BeforeItemCancelled(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}