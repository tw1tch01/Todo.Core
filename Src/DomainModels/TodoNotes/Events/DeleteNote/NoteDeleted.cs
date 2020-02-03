using System;

namespace Todo.DomainModels.TodoNotes.Events.DeleteNote
{
    public class NoteDeleted
    {
        public NoteDeleted(Guid noteId, DateTime deletedOn)
        {
            NoteId = noteId;
            DeletedOn = deletedOn;
        }

        public Guid NoteId { get; }
        public DateTime DeletedOn { get; }
    }
}