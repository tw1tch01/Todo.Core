﻿using System;
using Todo.DomainModels.TodoItems.Events.StartItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.StartItem
{
    public class ItemStartedProcess : ItemStarted, IWorkflowProcess
    {
        public ItemStartedProcess(Guid itemId, DateTime startedOn) : base(itemId, startedOn)
        {
        }
    }
}