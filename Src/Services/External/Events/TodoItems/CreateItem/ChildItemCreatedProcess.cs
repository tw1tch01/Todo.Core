using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class ChildItemCreatedProcess : ChildItemCreated, IWorkflowProcess
    {
        public ChildItemCreatedProcess(Guid parentItemId, Guid childItemId) : base(parentItemId, childItemId)
        {
        }
    }
}