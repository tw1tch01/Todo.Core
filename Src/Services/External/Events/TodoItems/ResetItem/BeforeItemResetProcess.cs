using System;
using Todo.DomainModels.TodoItems.Events.ResetItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.ResetItem
{
    public class BeforeItemResetProcess : BeforeItemReset, IWorkflowProcess
    {
        public BeforeItemResetProcess(Guid itemId) : base(itemId)
        {
        }
    }
}