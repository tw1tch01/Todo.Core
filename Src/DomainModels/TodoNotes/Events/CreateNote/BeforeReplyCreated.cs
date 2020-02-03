using System;

namespace Todo.DomainModels.TodoNotes.Events.CreateNote
{
    public class BeforeReplyCreated
    {
        public BeforeReplyCreated(Guid parentNoteId)
        {
            ParentNoteId = parentNoteId;
        }

        public Guid ParentNoteId { get; }
    }
}