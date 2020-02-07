using System;
using Todo.DomainModels.TodoItems.Events.UpdateItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.UpdateItem
{
    public class ItemUpdatedProcess : ItemUpdated, IWorkflowProcess
    {
        public ItemUpdatedProcess(Guid itemId, DateTime updatedOn) : base(itemId, updatedOn)
        {
        }
    }
}