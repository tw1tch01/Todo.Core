using System;
using Todo.DomainModels.TodoItems.Events.ResetItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.ResetItem
{
    public class BeforeItemResetProcess : BeforeItemReset, IWorkflowProcess
    {
        public BeforeItemResetProcess(Guid itemId) : base(itemId)
        {
        }
    }
}