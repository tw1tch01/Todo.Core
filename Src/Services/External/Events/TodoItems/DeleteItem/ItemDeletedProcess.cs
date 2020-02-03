using System;
using Todo.DomainModels.TodoItems.Events.DeleteItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.DeleteItem
{
    public class ItemDeletedProcess : ItemDeleted, IWorkflowProcess
    {
        public ItemDeletedProcess(Guid itemId, DateTime deletedOn) : base(itemId, deletedOn)
        {
        }
    }
}