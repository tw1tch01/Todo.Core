using System;
using Todo.DomainModels.TodoNotes.Events.DeleteNote;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoNotes.DeleteNote
{
    public class BeforeNoteDeletedProcess : BeforeNoteDeleted, IWorkflowProcess
    {
        public BeforeNoteDeletedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}