using System;
using Todo.DomainModels.Common;
using Todo.DomainModels.TodoItems.Events.ResetItem;

namespace Todo.Services.External.Events.TodoItems.ResetItem
{
    public class ItemResetNotification : ItemReset, IWorkflowProcess
    {
        public ItemResetNotification(Guid itemId, DateTime resetOn) : base(itemId, resetOn)
        {
        }
    }
}