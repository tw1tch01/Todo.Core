using System;

namespace Todo.Domain.Exceptions
{
    public class ItemPreviouslyCancelledException : Exception
    {
        private const string _message = "Item was previously cancelled on {0}. (ItemId: {1})";
        private const string _cancelledOnKey = "CancelledOn";
        private const string _itemIdKey = "ItemId";

        public ItemPreviouslyCancelledException(DateTime cancelledOn, Guid itemId)
            : base(string.Format(_message, cancelledOn, itemId))
        {
            Data[_cancelledOnKey] = cancelledOn;
            Data[_itemIdKey] = itemId;
        }
    }
}