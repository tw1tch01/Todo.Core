using System;
using Todo.DomainModels.TodoNotes.Events.UpdateNote;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoNotes.UpdateNote
{
    public class NoteUpdatedNotification : NoteUpdated, INotificationProcess
    {
        public NoteUpdatedNotification(Guid noteId) : base(noteId)
        {
        }
    }
}