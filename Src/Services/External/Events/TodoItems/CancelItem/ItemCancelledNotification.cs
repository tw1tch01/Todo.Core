using System;
using Todo.DomainModels.TodoItems.Events.CancelItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.CancelItem
{
    public class ItemCancelledNotification : ItemCancelled, INotificationProcess
    {
        public ItemCancelledNotification(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}