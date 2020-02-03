using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoNotes.CreateNote
{
    public class NotedCreatedNotification : NotedCreated, INotificationProcess
    {
        public NotedCreatedNotification(Guid noteId) : base(noteId)
        {
        }
    }
}