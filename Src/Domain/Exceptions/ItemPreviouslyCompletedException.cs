using System;

namespace Todo.Domain.Exceptions
{
    public class ItemPreviouslyCompletedException : Exception
    {
        private const string _message = "Item was previously completed on {0}. (ItemId: {1})";
        private const string _completedOnKey = "CompletedOn";
        private const string _itemIdKey = "ItemId";

        public ItemPreviouslyCompletedException(DateTime completedOn, Guid itemId)
            : base(string.Format(_message, completedOn, itemId))
        {
            Data[_completedOnKey] = completedOn;
            Data[_itemIdKey] = itemId;
        }
    }
}