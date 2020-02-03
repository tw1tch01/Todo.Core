using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.External.Notifications;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class ItemCreatedNotification : ItemCreated, INotificationProcess
    {
        public ItemCreatedNotification(Guid itemId) : base(itemId)
        {
        }
    }
}