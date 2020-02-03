using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class BeforeChildItemCreatedProcess : BeforeChildItemCreated, IWorkflowProcess
    {
        public BeforeChildItemCreatedProcess(Guid parentItemId) : base(parentItemId)
        {
        }
    }
}