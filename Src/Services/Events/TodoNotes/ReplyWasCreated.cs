using System;
using MediatR;

namespace Todo.Services.Events.TodoNotes
{
    public class ReplyWasCreated : INotification
    {
        public ReplyWasCreated(Guid parentNoteId, Guid replyNoteId)
        {
            ParentNoteId = parentNoteId;
            ReplyNoteId = replyNoteId;
        }

        public Guid ParentNoteId { get; }
        public Guid ReplyNoteId { get; }
    }
}