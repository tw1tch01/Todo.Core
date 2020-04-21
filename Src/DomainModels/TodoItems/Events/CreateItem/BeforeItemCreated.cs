using System;

namespace Todo.DomainModels.TodoItems.Events.CreateItem
{
    public class BeforeItemCreated
    {
        public BeforeItemCreated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}