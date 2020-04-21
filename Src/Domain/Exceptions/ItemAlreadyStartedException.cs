using System;

namespace Todo.Domain.Exceptions
{
    public class ItemAlreadyStartedException : Exception
    {
        private const string _message = "Item already started on {0}. (ItemId: {1})";
        private const string _startedOnKey = "StartedOn";
        private const string _itemIdKey = "ItemId";

        public ItemAlreadyStartedException(DateTime startedOn, Guid itemId)
            : base(string.Format(_message, startedOn, itemId))
        {
            Data[_startedOnKey] = startedOn;
            Data[_itemIdKey] = itemId;
        }
    }
}