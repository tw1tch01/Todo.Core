using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.DeleteItem
{
    public class ItemDeleted : IWorkflowProcess, INotificationProcess
    {
        public ItemDeleted(Guid itemId, DateTime deletedOn)
        {
            ItemId = itemId;
            DeletedOn = deletedOn;
        }

        public Guid ItemId { get; }
        public DateTime DeletedOn { get; }
    }
}