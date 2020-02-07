using System;
using Todo.DomainModels.TodoNotes.Events.UpdateNote;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Events.UpdateNote
{
    public class BeforeNoteUpdatedProcess : BeforeNoteUpdated, IWorkflowProcess
    {
        public BeforeNoteUpdatedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}