﻿using System;
using Todo.DomainModels.TodoItems.Events.CompleteItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.CompleteItem
{
    public class ItemCompletedProcess : ItemCompleted, IWorkflowProcess
    {
        public ItemCompletedProcess(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}