using System;
using MediatR;

namespace Todo.Services.Events.TodoItems
{
    public class ItemWasCompleted : INotification
    {
        public ItemWasCompleted(Guid itemId, DateTime completedOn)
        {
            ItemId = itemId;
            CompletedOn = completedOn;
        }

        public Guid ItemId { get; }
        public DateTime CompletedOn { get; }
    }
}