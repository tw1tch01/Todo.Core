using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.CreateItem
{
    public class BeforeItemCreated : IWorkflowProcess
    {
        public BeforeItemCreated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}