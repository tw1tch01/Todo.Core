using System;
using Todo.DomainModels.TodoItems.Events.UpdateItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.UpdateItem
{
    public class BeforeItemUpdatedProcess : BeforeItemUpdated, IWorkflowProcess
    {
        public BeforeItemUpdatedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}