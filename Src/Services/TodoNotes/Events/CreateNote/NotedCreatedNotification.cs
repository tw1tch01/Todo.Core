using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.Notifications;

namespace Todo.Services.TodoNotes.Events.CreateNote
{
    public class NotedCreatedNotification : NotedCreated, INotificationProcess
    {
        public NotedCreatedNotification(Guid noteId) : base(noteId)
        {
        }
    }
}