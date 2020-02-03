using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.CancelItem
{
    public class BeforeItemCancelled : IWorkflowProcess
    {
        public BeforeItemCancelled(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}