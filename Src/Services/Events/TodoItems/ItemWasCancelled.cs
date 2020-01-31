using System;
using MediatR;

namespace Todo.Services.Events.TodoItems
{
    public class ItemWasCancelled : INotification
    {
        public ItemWasCancelled(Guid itemId, DateTime cancelledOn)
        {
            ItemId = itemId;
            CancelledOn = cancelledOn;
        }

        public Guid ItemId { get; }

        public DateTime CancelledOn { get; }
    }
}