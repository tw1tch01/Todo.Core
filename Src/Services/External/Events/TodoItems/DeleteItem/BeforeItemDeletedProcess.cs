using System;
using Todo.DomainModels.TodoItems.Events.DeleteItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.DeleteItem
{
    public class BeforeItemDeletedProcess : BeforeItemDeleted, IWorkflowProcess
    {
        public BeforeItemDeletedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}