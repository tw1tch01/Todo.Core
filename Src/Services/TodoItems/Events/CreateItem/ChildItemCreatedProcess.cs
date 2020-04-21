using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.CreateItem
{
    public class ChildItemCreatedProcess : ChildItemCreated, IWorkflowProcess
    {
        public ChildItemCreatedProcess(Guid parentItemId, Guid childItemId) : base(parentItemId, childItemId)
        {
        }
    }
}