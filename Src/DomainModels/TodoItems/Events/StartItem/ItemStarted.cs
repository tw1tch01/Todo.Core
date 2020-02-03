using System;

namespace Todo.DomainModels.TodoItems.Events.StartItem
{
    public class ItemStarted
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