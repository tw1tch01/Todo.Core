using System;
using MediatR;

namespace Todo.Services.Events.TodoItems
{
    public class ItemWasStarted : INotification
    {
        public ItemWasStarted(Guid itemId, DateTime startedOn)
        {
            ItemId = itemId;
            StartedOn = startedOn;
        }

        public Guid ItemId { get; }
        public DateTime StartedOn { get; }
    }
}