using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.Notifications;

namespace Todo.Services.TodoItems.Events.CreateItem
{
    public class ChildItemCreatedNotification : ChildItemCreated, INotificationProcess
    {
        public ChildItemCreatedNotification(Guid parentItemId, Guid childItemId) : base(parentItemId, childItemId)
        {
        }
    }
}