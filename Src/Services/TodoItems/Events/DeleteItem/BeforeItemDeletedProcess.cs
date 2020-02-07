using System;
using Todo.DomainModels.TodoItems.Events.DeleteItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.DeleteItem
{
    public class BeforeItemDeletedProcess : BeforeItemDeleted, IWorkflowProcess
    {
        public BeforeItemDeletedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}