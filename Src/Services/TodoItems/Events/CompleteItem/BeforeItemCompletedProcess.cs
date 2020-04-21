using System;
using Todo.DomainModels.TodoItems.Events.CompleteItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.CompleteItem
{
    public class BeforeItemCompletedProcess : BeforeItemCompleted, IWorkflowProcess
    {
        public BeforeItemCompletedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}