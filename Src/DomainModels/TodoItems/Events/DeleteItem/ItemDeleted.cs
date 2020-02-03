using System;

namespace Todo.DomainModels.TodoItems.Events.DeleteItem
{
    public class ItemDeleted
    {
        public ItemDeleted(Guid itemId, DateTime deletedOn)
        {
            ItemId = itemId;
            DeletedOn = deletedOn;
        }

        public Guid ItemId { get; }
        public DateTime DeletedOn { get; }
    }
}