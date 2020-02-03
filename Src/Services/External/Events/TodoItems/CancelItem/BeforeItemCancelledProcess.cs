using System;
using Todo.DomainModels.TodoItems.Events.CancelItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.CancelItem
{
    public class BeforeItemCancelledProcess : BeforeItemCancelled, IWorkflowProcess
    {
        public BeforeItemCancelledProcess(Guid itemId) : base(itemId)
        {
        }
    }
}