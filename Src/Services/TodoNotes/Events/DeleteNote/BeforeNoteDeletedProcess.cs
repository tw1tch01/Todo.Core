using System;
using Todo.DomainModels.TodoNotes.Events.DeleteNote;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Events.DeleteNote
{
    public class BeforeNoteDeletedProcess : BeforeNoteDeleted, IWorkflowProcess
    {
        public BeforeNoteDeletedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}