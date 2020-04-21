using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoNotes.Validation
{
    public class NoteUpdatedResult : NoteValidResult
    {
        private const string _message = "Note successfully updated.";
        private const string _modifiedOnKey = nameof(TodoItemNote.ModifiedOn);

        public NoteUpdatedResult(Guid noteId, DateTime modifiedOn)
            : base(noteId, _message)
        {
            Data[_modifiedOnKey] = modifiedOn;
        }
    }
}