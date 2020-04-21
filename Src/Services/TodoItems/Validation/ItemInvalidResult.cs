using System;

namespace Todo.Services.TodoItems.Validation
{
    public abstract class ItemInvalidResult : ItemValidationResult
    {
        protected ItemInvalidResult(string message)
            : base(false, message)
        {
        }

        protected ItemInvalidResult(Guid itemId, string message)
            : base(itemId, false, message)
        {
        }
    }
}