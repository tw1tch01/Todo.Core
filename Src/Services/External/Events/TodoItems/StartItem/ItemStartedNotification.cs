using System;
using Todo.DomainModels.TodoItems.Events.StartItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.StartItem
{
    public class ItemStartedNotification : ItemStarted, INotificationProcess
    {
        public ItemStartedNotification(Guid itemId, DateTime startedOn) : base(itemId, startedOn)
        {
        }
    }
}