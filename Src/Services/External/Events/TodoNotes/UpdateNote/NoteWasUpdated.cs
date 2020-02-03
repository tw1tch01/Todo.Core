using System;
using MediatR;

namespace Todo.Services.Events.TodoNotes
{
    public class NoteWasUpdated : INotification
    {
        public NoteWasUpdated(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }
    }
}