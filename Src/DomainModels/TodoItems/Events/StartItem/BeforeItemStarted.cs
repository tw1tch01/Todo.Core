using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.StartItem
{
    public class BeforeItemStarted : IWorkflowProcess
    {
        public BeforeItemStarted(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}