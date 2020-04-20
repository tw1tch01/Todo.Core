using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoNotes.Validation
{
    public class ItemNotFoundResult : NoteInvalidResult
    {
        private const string _message = "Item record was not found.";
        private const string _itemIdKey = nameof(TodoItemNote.ItemId);

        public ItemNotFoundResult(Guid itemId)
            : base(Guid.Empty, _message)
        {
            Data[_itemIdKey] = itemId;
        }
    }
}