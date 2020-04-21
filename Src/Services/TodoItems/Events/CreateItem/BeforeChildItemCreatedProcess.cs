using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.CreateItem
{
    public class BeforeChildItemCreatedProcess : BeforeChildItemCreated, IWorkflowProcess
    {
        public BeforeChildItemCreatedProcess(Guid parentItemId) : base(parentItemId)
        {
        }
    }
}