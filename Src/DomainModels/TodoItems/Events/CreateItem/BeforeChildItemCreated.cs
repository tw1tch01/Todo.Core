using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.CreateItem
{
    public class BeforeChildItemCreated : IWorkflowProcess
    {
        public BeforeChildItemCreated(Guid parentItemId)
        {
            ParentItemId = parentItemId;
        }

        public Guid ParentItemId { get; }
    }
}