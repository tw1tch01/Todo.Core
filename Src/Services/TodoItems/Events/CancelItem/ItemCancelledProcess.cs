using System;
using Todo.DomainModels.TodoItems.Events.CancelItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.CancelItem
{
    public class ItemCancelledProcess : ItemCancelled, IWorkflowProcess
    {
        public ItemCancelledProcess(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}