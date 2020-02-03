using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.External.Notifications;

namespace Todo.Services.External.Events.TodoNotes.CreateNote
{
    public class ReplyCreatedNotification : ReplyCreated, INotificationProcess
    {
        public ReplyCreatedNotification(Guid parentNoteId, Guid replyNoteId) : base(parentNoteId, replyNoteId)
        {
        }
    }
}