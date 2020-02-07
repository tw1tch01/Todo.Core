using System;
using Todo.DomainModels.TodoItems.Events.UpdateItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.UpdateItem
{
    public class BeforeItemUpdatedProcess : BeforeItemUpdated, IWorkflowProcess
    {
        public BeforeItemUpdatedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}