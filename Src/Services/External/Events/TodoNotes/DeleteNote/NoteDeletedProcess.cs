using System;
using Todo.DomainModels.TodoNotes.Events.DeleteNote;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoNotes.DeleteNote
{
    public class NoteDeletedProcess : NoteDeleted, IWorkflowProcess
    {
        public NoteDeletedProcess(Guid noteId, DateTime deletedOn) : base(noteId, deletedOn)
        {
        }
    }
}