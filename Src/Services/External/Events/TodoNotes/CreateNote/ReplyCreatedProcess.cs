using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoNotes.CreateNote
{
    public class ReplyCreatedProcess : ReplyCreated, IWorkflowProcess
    {
        public ReplyCreatedProcess(Guid parentNoteId, Guid replyNoteId) : base(parentNoteId, replyNoteId)
        {
        }
    }
}