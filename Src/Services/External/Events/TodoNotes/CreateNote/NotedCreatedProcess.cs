using System;
using Todo.DomainModels.TodoNotes.Events.CreateNote;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoNotes.CreateNote
{
    public class NotedCreatedProcess : NotedCreated, IWorkflowProcess
    {
        public NotedCreatedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}