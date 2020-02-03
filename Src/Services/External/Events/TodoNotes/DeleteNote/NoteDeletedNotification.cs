using System;
using Todo.DomainModels.TodoNotes.Events.DeleteNote;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoNotes.DeleteNote
{
    public class NoteDeletedNotification : NoteDeleted, INotificationProcess
    {
        public NoteDeletedNotification(Guid noteId, DateTime deletedOn) : base(noteId, deletedOn)
        {
        }
    }
}