using System;
using Todo.DomainModels.TodoItems.Events.CompleteItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.CompleteItem
{
    public class BeforeItemCompletedProcess : BeforeItemCompleted, IWorkflowProcess
    {
        public BeforeItemCompletedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}