using System;
using Todo.DomainModels.TodoNotes.Events.DeleteNote;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoNotes.DeleteNote
{
    public class BeforeNoteDeletedProcess : BeforeNoteDeleted, IWorkflowProcess
    {
        public BeforeNoteDeletedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}