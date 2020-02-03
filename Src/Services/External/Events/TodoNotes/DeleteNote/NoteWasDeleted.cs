using System;
using MediatR;

namespace Todo.Services.Events.TodoNotes
{
    public class NoteWasDeleted : INotification
    {
        public NoteWasDeleted(Guid noteId, DateTime deletedOn)
        {
            NoteId = noteId;
            DeletedOn = deletedOn;
        }

        public Guid NoteId { get; }
        public DateTime DeletedOn { get; }
    }
}