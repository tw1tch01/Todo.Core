using System;
using Todo.DomainModels.TodoItems.Events.CompleteItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.CompleteItem
{
    public class ItemCompletedProcess : ItemCompleted, IWorkflowProcess
    {
        public ItemCompletedProcess(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}