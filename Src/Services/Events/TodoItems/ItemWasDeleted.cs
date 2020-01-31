using System;
using MediatR;

namespace Todo.Services.Events.TodoItems
{
    public class ItemWasDeleted : INotification
    {
        public ItemWasDeleted(Guid itemId, DateTime deletedOn)
        {
            ItemId = itemId;
            DeletedOn = deletedOn;
        }

        public Guid ItemId { get; }
        public DateTime DeletedOn { get; }
    }
}