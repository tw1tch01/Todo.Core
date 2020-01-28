using System;

namespace Todo.Domain.Exceptions
{
    public class ItemPreviouslyCompletedException : Exception
    {
        private const string _message = "Item was previously completed on {0}. (ItemId: {1})";

        public ItemPreviouslyCompletedException(DateTime completedOn, Guid itemId)
            : base(string.Format(_message, completedOn, itemId))
        {
            CompletedOn = completedOn;
            ItemId = itemId;
        }

        public DateTime CompletedOn { get; set; }
        public Guid ItemId { get; }
    }
}