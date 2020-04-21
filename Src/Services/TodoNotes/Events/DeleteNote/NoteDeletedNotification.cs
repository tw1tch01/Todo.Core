using System;
using Todo.DomainModels.TodoNotes.Events.DeleteNote;
using Todo.Services.Notifications;

namespace Todo.Services.TodoNotes.Events.DeleteNote
{
    public class NoteDeletedNotification : NoteDeleted, INotificationProcess
    {
        public NoteDeletedNotification(Guid noteId, DateTime deletedOn) : base(noteId, deletedOn)
        {
        }
    }
}