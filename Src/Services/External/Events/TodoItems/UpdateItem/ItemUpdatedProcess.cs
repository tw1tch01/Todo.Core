﻿using System;
using Todo.DomainModels.TodoItems.Events.UpdateItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.UpdateItem
{
    public class ItemUpdatedProcess : ItemUpdated, IWorkflowProcess
    {
        public ItemUpdatedProcess(Guid itemId, DateTime updatedOn) : base(itemId, updatedOn)
        {
        }
    }
}