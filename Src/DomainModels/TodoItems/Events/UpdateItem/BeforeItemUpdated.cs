using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.UpdateItem
{
    public class BeforeItemUpdated : IWorkflowProcess
    {
        public BeforeItemUpdated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}