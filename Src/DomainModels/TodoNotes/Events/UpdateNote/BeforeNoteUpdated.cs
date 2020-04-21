using System;

namespace Todo.DomainModels.TodoNotes.Events.UpdateNote
{
    public class BeforeNoteUpdated
    {
        public BeforeNoteUpdated(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }
    }
}