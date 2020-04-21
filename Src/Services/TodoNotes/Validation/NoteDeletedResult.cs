using System;

namespace Todo.Services.TodoNotes.Validation
{
    internal class NoteDeletedResult : NoteValidResult
    {
        private const string _message = "Note successfully deleted.";
        private const string _deletedOnKey = "DeletedOn";

        public NoteDeletedResult(Guid noteId, DateTime deletedOn)
            : base(noteId, _message)
        {
            Data[_deletedOnKey] = deletedOn;
        }
    }
}