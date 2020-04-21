using System;

namespace Todo.DomainModels.TodoNotes.Events.DeleteNote
{
    public class BeforeNoteDeleted
    {
        public BeforeNoteDeleted(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }
    }
}