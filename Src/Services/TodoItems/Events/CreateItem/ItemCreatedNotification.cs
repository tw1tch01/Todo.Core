using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.Notifications;

namespace Todo.Services.TodoItems.Events.CreateItem
{
    public class ItemCreatedNotification : ItemCreated, INotificationProcess
    {
        public ItemCreatedNotification(Guid itemId) : base(itemId)
        {
        }
    }
}