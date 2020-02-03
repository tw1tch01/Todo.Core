using System;

namespace Todo.DomainModels.TodoNotes.Events.UpdateNote
{
    public class NoteUpdated
    {
        public NoteUpdated(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }
    }
}