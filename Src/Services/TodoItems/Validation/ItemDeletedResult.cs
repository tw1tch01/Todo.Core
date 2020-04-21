using System;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemDeletedResult : ItemValidResult
    {
        private const string _message = "Item successfully deleted.";
        private const string _deletedOnKey = "DeletedOn";

        public ItemDeletedResult(Guid itemId, DateTime deletedOn)
            : base(itemId, _message)
        {
            Data[_deletedOnKey] = deletedOn;
        }
    }
}