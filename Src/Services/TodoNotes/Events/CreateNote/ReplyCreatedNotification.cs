using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.Notifications;

namespace Todo.Services.TodoNotes.Events.CreateNote
{
    public class ReplyCreatedNotification : ReplyCreated, INotificationProcess
    {
        public ReplyCreatedNotification(Guid parentNoteId, Guid replyNoteId) : base(parentNoteId, replyNoteId)
        {
        }
    }
}