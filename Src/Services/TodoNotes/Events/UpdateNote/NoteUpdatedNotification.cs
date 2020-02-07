using System;
using Todo.DomainModels.TodoNotes.Events.UpdateNote;
using Todo.Services.Notifications;

namespace Todo.Services.TodoNotes.Events.UpdateNote
{
    public class NoteUpdatedNotification : NoteUpdated, INotificationProcess
    {
        public NoteUpdatedNotification(Guid noteId) : base(noteId)
        {
        }
    }
}