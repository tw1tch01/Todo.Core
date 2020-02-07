using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Events.CreateNote
{
    public class BeforeReplyCreatedProcess : BeforeReplyCreated, IWorkflowProcess
    {
        public BeforeReplyCreatedProcess(Guid parentNoteId) : base(parentNoteId)
        {
        }
    }
}