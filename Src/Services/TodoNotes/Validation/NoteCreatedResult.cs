using System;
using Todo.Domain.Entities;

namespace Todo.Services.TodoNotes.Validation
{
    public class NoteCreatedResult : NoteValidResult
    {
        private const string _message = "Note successfully created.";
        private const string _createdOnKey = nameof(TodoItemNote.CreatedOn);

        public NoteCreatedResult(Guid noteId, DateTime createdOn)
            : base(noteId, _message)
        {
            Data[_createdOnKey] = createdOn;
        }
    }
}