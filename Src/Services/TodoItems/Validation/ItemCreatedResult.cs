using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemCreatedResult : ItemValidResult
    {
        private const string _message = "Item successfully created.";
        private const string _createdOnKey = nameof(TodoItem.CreatedOn);

        public ItemCreatedResult(Guid itemId, DateTime createdOn)
            : base(itemId, _message)
        {
            Data[_createdOnKey] = createdOn;
        }
    }
}