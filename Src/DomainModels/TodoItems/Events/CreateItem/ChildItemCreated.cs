using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.CreateItem
{
    public class ChildItemCreated : IWorkflowProcess, INotificationProcess
    {
        public ChildItemCreated(Guid parentItemId, Guid childItemId)
        {
            ParentItemId = parentItemId;
            ChildItemId = childItemId;
        }

        public Guid ParentItemId { get; }
        public Guid ChildItemId { get; }
    }
}