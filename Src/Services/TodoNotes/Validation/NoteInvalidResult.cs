using System;

namespace Todo.Services.TodoNotes.Validation
{
    public class NoteInvalidResult : NoteValidationResult
    {
        public NoteInvalidResult(Guid noteId, string message)
            : base(noteId, false, message)
        {
        }

        public NoteInvalidResult(string message)
            : base(false, message)
        {
        }
    }
}