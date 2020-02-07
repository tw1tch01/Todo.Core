using System;
using Todo.DomainModels.TodoItems.Events.DeleteItem;
using Todo.Services.Notifications;

namespace Todo.Services.TodoItems.Events.DeleteItem
{
    public class ItemDeletedNotification : ItemDeleted, INotificationProcess
    {
        public ItemDeletedNotification(Guid itemId, DateTime deletedOn) : base(itemId, deletedOn)
        {
        }
    }
}