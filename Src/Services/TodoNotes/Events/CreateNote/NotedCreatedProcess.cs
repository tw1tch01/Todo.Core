using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.Workflows;

namespace Todo.Services.TodoNotes.Events.CreateNote
{
    public class NotedCreatedProcess : NotedCreated, IWorkflowProcess
    {
        public NotedCreatedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}