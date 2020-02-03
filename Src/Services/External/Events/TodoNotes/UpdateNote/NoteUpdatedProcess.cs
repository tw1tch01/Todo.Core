﻿using System;
using Todo.DomainModels.TodoNotes.Events.UpdateNote;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoNotes.UpdateNote
{
    public class NoteUpdatedProcess : NoteUpdated, IWorkflowProcess
    {
        public NoteUpdatedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}