using System;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemNotFoundResult : ItemInvalidResult
    {
        private const string _message = "Item record was not found.";

        public ItemNotFoundResult(Guid itemId)
            : base(itemId, _message)
        {
        }
    }
}