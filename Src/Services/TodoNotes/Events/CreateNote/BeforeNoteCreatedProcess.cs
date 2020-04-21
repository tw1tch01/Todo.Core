using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Events.CreateNote
{
    public class BeforeNoteCreatedProcess : BeforeNoteCreated, IWorkflowProcess
    {
        public BeforeNoteCreatedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}