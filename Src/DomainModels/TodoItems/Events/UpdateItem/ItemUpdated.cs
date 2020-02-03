using System;

namespace Todo.DomainModels.TodoItems.Events.UpdateItem
{
    public class ItemUpdated
    {
        public ItemUpdated(Guid itemId, DateTime updatedOn)
        {
            ItemId = itemId;
            UpdatedOn = updatedOn;
        }

        public Guid ItemId { get; }
        public DateTime UpdatedOn { get; }
    }
}