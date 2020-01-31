using System;
using MediatR;

namespace Todo.Services.Events.TodoItems
{
    public class ItemWasUpdated : INotification
    {
        public ItemWasUpdated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}