using System;
using Todo.DomainModels.TodoNotes.Events.UpdateNote;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Events.UpdateNote
{
    public class NoteUpdatedProcess : NoteUpdated, IWorkflowProcess
    {
        public NoteUpdatedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}