using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.CompleteItem
{
    public class ItemCompleted : IWorkflowProcess, INotificationProcess
    {
        public ItemCompleted(Guid itemId, DateTime cancelledOn)
        {
            ItemId = itemId;
            CancelledOn = cancelledOn;
        }

        public Guid ItemId { get; }
        public DateTime CancelledOn { get; }
    }
}