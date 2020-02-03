using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.ResetItem
{
    public class BeforeItemReset : IWorkflowProcess
    {
        public BeforeItemReset(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}