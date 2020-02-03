using System;
using Todo.DomainModels.TodoItems.Events.CancelItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.CancelItem
{
    public class ItemCancelledProcess : ItemCancelled, IWorkflowProcess
    {
        public ItemCancelledProcess(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}