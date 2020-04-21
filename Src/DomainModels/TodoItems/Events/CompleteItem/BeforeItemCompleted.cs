using System;

namespace Todo.DomainModels.TodoItems.Events.CompleteItem
{
    public class BeforeItemCompleted
    {
        public BeforeItemCompleted(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}