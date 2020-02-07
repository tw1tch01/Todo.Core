using System;
using Todo.DomainModels.TodoNotes.Events.DeleteNote;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Events.DeleteNote
{
    public class NoteDeletedProcess : NoteDeleted, IWorkflowProcess
    {
        public NoteDeletedProcess(Guid noteId, DateTime deletedOn) : base(noteId, deletedOn)
        {
        }
    }
}