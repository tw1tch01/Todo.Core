using System;

namespace Todo.Services.TodoItems.Validation
{
    public abstract class ItemValidResult : ItemValidationResult
    {
        protected ItemValidResult(Guid itemId, string message)
            : base(itemId, true, message)
        {
        }
    }
}