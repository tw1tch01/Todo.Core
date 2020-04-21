using System;
using Todo.DomainModels.TodoItems.Events.ResetItem;
using Todo.Services.Notifications;

namespace Todo.Services.TodoItems.Events.ResetItem
{
    public class ItemResetNotification : ItemReset, INotificationProcess
    {
        public ItemResetNotification(Guid itemId, DateTime resetOn) : base(itemId, resetOn)
        {
        }
    }
}