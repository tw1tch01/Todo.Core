using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.CompleteItem
{
    public class BeforeItemCompleted : IWorkflowProcess
    {
        public BeforeItemCompleted(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}