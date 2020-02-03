using System;

namespace Todo.DomainModels.TodoNotes.Events.CreateNote
{
    public class ReplyCreated
    {
        public ReplyCreated(Guid parentNoteId, Guid replyNoteId)
        {
            ParentNoteId = parentNoteId;
            ReplyNoteId = replyNoteId;
        }

        public Guid ParentNoteId { get; }
        public Guid ReplyNoteId { get; }
    }
}