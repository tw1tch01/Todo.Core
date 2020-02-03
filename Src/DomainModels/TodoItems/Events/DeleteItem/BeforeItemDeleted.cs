using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.DeleteItem
{
    public class BeforeItemDeleted : IWorkflowProcess, INotificationProcess
    {
        public BeforeItemDeleted(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}