using System;
using Todo.DomainModels.TodoItems.Events.CancelItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.CancelItem
{
    public class BeforeItemCancelledProcess : BeforeItemCancelled, IWorkflowProcess
    {
        public BeforeItemCancelledProcess(Guid itemId) : base(itemId)
        {
        }
    }
}