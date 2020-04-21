using System;

namespace Todo.DomainModels.TodoItems.Events.UpdateItem
{
    public class BeforeItemUpdated
    {
        public BeforeItemUpdated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}