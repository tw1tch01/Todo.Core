using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemUpdatedResult : ItemValidResult
    {
        private const string _message = "Item successfully updated.";
        private const string _modifiedOnKey = nameof(TodoItem.ModifiedOn);

        public ItemUpdatedResult(Guid itemId, DateTime modifiedOn)
            : base(itemId, _message)
        {
            Data[_modifiedOnKey] = modifiedOn;
        }
    }
}