using System;

namespace Todo.DomainModels.TodoItems.Events.CreateItem
{
    public class ItemCreated
    {
        public ItemCreated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}