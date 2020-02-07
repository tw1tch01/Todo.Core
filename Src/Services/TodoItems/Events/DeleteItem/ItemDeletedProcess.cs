using System;
using Todo.DomainModels.TodoItems.Events.DeleteItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.DeleteItem
{
    public class ItemDeletedProcess : ItemDeleted, IWorkflowProcess
    {
        public ItemDeletedProcess(Guid itemId, DateTime deletedOn) : base(itemId, deletedOn)
        {
        }
    }
}