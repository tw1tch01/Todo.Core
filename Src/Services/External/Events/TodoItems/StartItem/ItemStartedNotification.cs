using System;
using Todo.DomainModels.Common;
using Todo.DomainModels.TodoItems.Events.StartItem;

namespace Todo.Services.External.Events.TodoItems.StartItem
{
    public class ItemStartedNotification : ItemStarted, IWorkflowProcess
    {
        public ItemStartedNotification(Guid itemId, DateTime startedOn) : base(itemId, startedOn)
        {
        }
    }
}