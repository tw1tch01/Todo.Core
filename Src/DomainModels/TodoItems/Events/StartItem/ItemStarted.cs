using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.StartItem
{
    public class ItemStarted : IWorkflowProcess, INotificationProcess
    {
        public ItemStarted(Guid itemId, DateTime startedOn)
        {
            ItemId = itemId;
            StartedOn = startedOn;
        }

        public Guid ItemId { get; }
        public DateTime StartedOn { get; }
    }
}