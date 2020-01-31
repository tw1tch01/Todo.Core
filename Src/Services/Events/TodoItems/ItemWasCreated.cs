using System;
using MediatR;

namespace Todo.Services.Events.TodoItems
{
    public class ItemWasCreated : INotification
    {
        public ItemWasCreated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}