using System;
using Todo.DomainModels.TodoItems.Events.StartItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.StartItem
{
    public class ItemStartedProcess : ItemStarted, IWorkflowProcess
    {
        public ItemStartedProcess(Guid itemId, DateTime startedOn) : base(itemId, startedOn)
        {
        }
    }
}