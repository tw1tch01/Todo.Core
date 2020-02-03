using System;
using Todo.DomainModels.TodoItems.Events.StartItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.StartItem
{
    public class BeforeItemStartedProcess : BeforeItemStarted, IWorkflowProcess
    {
        public BeforeItemStartedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}