using System;

namespace Todo.DomainModels.TodoItems.Events.DeleteItem
{
    public class BeforeItemDeleted
    {
        public BeforeItemDeleted(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}