using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.ResetItem
{
    public class ItemReset : IWorkflowProcess, INotificationProcess
    {
        public ItemReset(Guid itemId, DateTime resetOn)
        {
            ItemId = itemId;
            ResetOn = resetOn;
        }

        public Guid ItemId { get; }
        public DateTime ResetOn { get; }
    }
}