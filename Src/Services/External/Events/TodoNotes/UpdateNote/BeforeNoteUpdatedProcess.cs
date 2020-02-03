﻿using System;
using Todo.DomainModels.TodoNotes.Events.UpdateNote;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoNotes.UpdateNote
{
    public class BeforeNoteUpdatedProcess : BeforeNoteUpdated, IWorkflowProcess
    {
        public BeforeNoteUpdatedProcess(Guid noteId) : base(noteId)
        {
        }
    }
}