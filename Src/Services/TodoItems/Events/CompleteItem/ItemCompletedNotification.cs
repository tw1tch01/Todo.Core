using System;
using Todo.DomainModels.TodoItems.Events.CompleteItem;
using Todo.Services.Notifications;

namespace Todo.Services.TodoItems.Events.CompleteItem
{
    public class ItemCompletedNotification : ItemCompleted, INotificationProcess
    {
        public ItemCompletedNotification(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}