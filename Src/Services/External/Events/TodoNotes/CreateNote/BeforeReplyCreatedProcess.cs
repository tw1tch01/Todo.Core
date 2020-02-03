using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoNotes.CreateNote
{
    public class BeforeReplyCreatedProcess : BeforeReplyCreated, IWorkflowProcess
    {
        public BeforeReplyCreatedProcess(Guid parentNoteId) : base(parentNoteId)
        {
        }
    }
}