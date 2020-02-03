using System;
using Todo.DomainModels.TodoItems.Events.ResetItem;
using Todo.Services.External.Notifications;

namespace Todo.Services.External.Events.TodoItems.ResetItem
{
    public class ItemResetNotification : ItemReset, INotificationProcess
    {
        public ItemResetNotification(Guid itemId, DateTime resetOn) : base(itemId, resetOn)
        {
        }
    }
}