using System;

namespace Todo.DomainModels.TodoItems.Events.ResetItem
{
    public class BeforeItemReset
    {
        public BeforeItemReset(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}