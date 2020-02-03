using System;
using Todo.DomainModels.Common;
using Todo.DomainModels.TodoItems.Events.UpdateItem;

namespace Todo.Services.External.Events.TodoItems.UpdateItem
{
    public class ItemUpdatedNotification : ItemUpdated, IWorkflowProcess
    {
        public ItemUpdatedNotification(Guid itemId, DateTime updatedOn) : base(itemId, updatedOn)
        {
        }
    }
}