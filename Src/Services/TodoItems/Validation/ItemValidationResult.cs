using System;
using Todo.Domain.Entities;
using Todo.Services.Common.Validation;

namespace Todo.Services.TodoItems.Validation
{
    public abstract class ItemValidationResult : ValidationResult
    {
        private const string _itemIdKey = nameof(TodoItem.ItemId);

        protected ItemValidationResult(bool isValid, string message)
            : base(isValid, message)
        {
        }

        protected ItemValidationResult(Guid itemId, bool isValid, string message)
            : base(isValid, message)
        {
            Data[_itemIdKey] = itemId;
        }
    }
}