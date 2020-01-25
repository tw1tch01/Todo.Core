using System;

namespace Todo.Domain.Exceptions
{
    public class ItemPreviouslyCancelledException : Exception
    {
        private const string _message = "Item was previously cancelled on {0}. (ItemId: {1})";

        public ItemPreviouslyCancelledException(DateTime cancelledOn, Guid itemId)
            : base(string.Format(_message, cancelledOn, itemId))
        {
            CancelledOn = cancelledOn;
            ItemId = itemId;
        }

        public DateTime CancelledOn { get; set; }
        public Guid ItemId { get; }
    }
}