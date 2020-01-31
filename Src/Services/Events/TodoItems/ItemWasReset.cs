using System;
using MediatR;

namespace Todo.Services.Events.TodoItems
{
    public class ItemWasReset : INotification
    {
        public ItemWasReset(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}