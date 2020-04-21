using System;

namespace Todo.Services.TodoNotes.Validation
{
    public class NoteNotFoundResult : NoteInvalidResult
    {
        private const string _message = "Note record was not found.";

        public NoteNotFoundResult(Guid noteId)
            : base(noteId, _message)
        {
        }
    }
}