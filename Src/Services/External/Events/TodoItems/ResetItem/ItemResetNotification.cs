using System;
using Todo.DomainModels.TodoItems.Events.ResetItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.ResetItem
{
    public class ItemResetNotification : ItemReset, INotificationProcess
    {
        public ItemResetNotification(Guid itemId, DateTime resetOn) : base(itemId, resetOn)
        {
        }
    }
}