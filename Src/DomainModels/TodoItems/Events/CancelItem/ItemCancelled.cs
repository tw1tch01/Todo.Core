using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.CancelItem
{
    public class ItemCancelled : IWorkflowProcess, INotificationProcess
    {
        public ItemCancelled(Guid itemId, DateTime cancelledOn)
        {
            ItemId = itemId;
            CancelledOn = cancelledOn;
        }

        public Guid ItemId { get; }

        public DateTime CancelledOn { get; }
    }
}