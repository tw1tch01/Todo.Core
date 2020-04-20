using System;

namespace Todo.Services.TodoNotes.Validation
{
    public class NoteValidResult : NoteValidationResult
    {
        public NoteValidResult(Guid noteId, string message)
            : base(noteId, true, message)
        {
        }
    }
}