using System;
using Todo.DomainModels.TodoItems.Events.UpdateItem;
using Todo.Services.Notifications;

namespace Todo.Services.TodoItems.Events.UpdateItem
{
    public class ItemUpdatedNotification : ItemUpdated, INotificationProcess
    {
        public ItemUpdatedNotification(Guid itemId, DateTime updatedOn) : base(itemId, updatedOn)
        {
        }
    }
}