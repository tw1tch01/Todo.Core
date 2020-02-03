using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.UpdateItem
{
    public class ItemUpdated : IWorkflowProcess, INotificationProcess
    {
        public ItemUpdated(Guid itemId, DateTime updatedOn)
        {
            ItemId = itemId;
            UpdatedOn = updatedOn;
        }

        public Guid ItemId { get; }
        public DateTime UpdatedOn { get; }
    }
}