using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Events.CreateNote
{
    public class ReplyCreatedProcess : ReplyCreated, IWorkflowProcess
    {
        public ReplyCreatedProcess(Guid parentNoteId, Guid replyNoteId) : base(parentNoteId, replyNoteId)
        {
        }
    }
}