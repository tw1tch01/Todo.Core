using System;
using Todo.Domain.Entities;
using Todo.Services.Common.Validation;

namespace Todo.Services.TodoNotes.Validation
{
    public abstract class NoteValidationResult : ValidationResult
    {
        private const string _noteIdKey = nameof(TodoItemNote.NoteId);

        protected NoteValidationResult(bool isValid, string message)
            : base(isValid, message)
        {
        }

        protected NoteValidationResult(Guid noteId, bool isValid, string message)
            : base(isValid, message)
        {
            Data[_noteIdKey] = noteId;
        }
    }
}