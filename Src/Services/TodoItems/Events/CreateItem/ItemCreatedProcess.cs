using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.CreateItem
{
    public class ItemCreatedProcess : ItemCreated, IWorkflowProcess
    {
        public ItemCreatedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}