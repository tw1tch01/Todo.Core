using System;
using Todo.DomainModels.Common;
using Todo.DomainModels.TodoItems.Events.StartItem;

namespace Todo.Services.External.Events.TodoItems.StartItem
{
    public class ItemStartedWorkflow : ItemStarted, IWorkflowProcess
    {
        public ItemStartedWorkflow(Guid itemId, DateTime startedOn) : base(itemId, startedOn)
        {
        }
    }
}