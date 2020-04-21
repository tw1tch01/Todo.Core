using System;
using Todo.DomainModels.TodoItems.Events.StartItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.StartItem
{
    public class BeforeItemStartedProcess : BeforeItemStarted, IWorkflowProcess
    {
        public BeforeItemStartedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}