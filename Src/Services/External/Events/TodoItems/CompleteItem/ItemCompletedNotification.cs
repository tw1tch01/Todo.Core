using System;
using Todo.DomainModels.TodoItems.Events.CompleteItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.CompleteItem
{
    public class ItemCompletedNotification : ItemCompleted, INotificationProcess
    {
        public ItemCompletedNotification(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}