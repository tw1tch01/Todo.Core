using System;

namespace Todo.DomainModels.TodoNotes.Events.CreateNote
{
    public class NotedCreated
    {
        public NotedCreated(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }
    }
}