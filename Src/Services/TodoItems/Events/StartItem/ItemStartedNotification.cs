using System;
using Todo.DomainModels.TodoItems.Events.StartItem;
using Todo.Services.Notifications;

namespace Todo.Services.TodoItems.Events.StartItem
{
    public class ItemStartedNotification : ItemStarted, INotificationProcess
    {
        public ItemStartedNotification(Guid itemId, DateTime startedOn) : base(itemId, startedOn)
        {
        }
    }
}