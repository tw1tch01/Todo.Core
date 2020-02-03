using System;
using MediatR;

namespace Todo.Services.Events.TodoNotes
{
    public class NoteWasCreated : INotification
    {
        public NoteWasCreated(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }
    }
}