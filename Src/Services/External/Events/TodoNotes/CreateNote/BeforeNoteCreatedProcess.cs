using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoNotes.CreateNote
{
    public class BeforeNoteCreatedProcess : BeforeNoteCreated, IWorkflowProcess
    {
        public BeforeNoteCreatedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}