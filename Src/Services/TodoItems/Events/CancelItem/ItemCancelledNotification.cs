using System;
using Todo.DomainModels.TodoItems.Events.CancelItem;
using Todo.Services.Notifications;

namespace Todo.Services.TodoItems.Events.CancelItem
{
    public class ItemCancelledNotification : ItemCancelled, INotificationProcess
    {
        public ItemCancelledNotification(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}