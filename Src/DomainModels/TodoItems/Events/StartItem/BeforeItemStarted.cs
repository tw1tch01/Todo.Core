using System;

namespace Todo.DomainModels.TodoItems.Events.StartItem
{
    public class BeforeItemStarted
    {
        public BeforeItemStarted(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}