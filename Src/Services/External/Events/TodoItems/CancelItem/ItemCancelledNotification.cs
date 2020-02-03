using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CancelItem;

namespace Todo.Services.External.Events.TodoItems.CancelItem
{
    public class ItemCancelledNotification : ItemCancelled, INotification
    {
        public ItemCancelledNotification(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}