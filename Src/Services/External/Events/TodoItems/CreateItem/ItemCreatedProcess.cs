using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class ItemCreatedProcess : ItemCreated, IWorkflowProcess
    {
        public ItemCreatedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}