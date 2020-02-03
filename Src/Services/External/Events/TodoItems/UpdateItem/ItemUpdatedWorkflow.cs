using System;
using Todo.DomainModels.Common;
using Todo.DomainModels.TodoItems.Events.UpdateItem;

namespace Todo.Services.External.Events.TodoItems.UpdateItem
{
    public class ItemUpdatedWorkflow : ItemUpdated, IWorkflowProcess
    {
        public ItemUpdatedWorkflow(Guid itemId, DateTime updatedOn) : base(itemId, updatedOn)
        {
        }
    }
}