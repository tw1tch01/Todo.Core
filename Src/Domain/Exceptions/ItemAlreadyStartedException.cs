using System;

namespace Todo.Domain.Exceptions
{
    public class ItemAlreadyStartedException : Exception
    {
        private const string _message = "Item already started on {0}. (ItemId: {1})";

        public ItemAlreadyStartedException(DateTime cancelledOn, Guid itemId)
            : base(string.Format(_message, cancelledOn, itemId))
        {
            CancelledOn = cancelledOn;
            ItemId = itemId;
        }

        public DateTime CancelledOn { get; set; }
        public Guid ItemId { get; }
    }
}