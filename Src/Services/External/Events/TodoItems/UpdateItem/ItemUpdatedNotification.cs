﻿using System;
using Todo.DomainModels.TodoItems.Events.UpdateItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.UpdateItem
{
    public class ItemUpdatedNotification : ItemUpdated, INotificationProcess
    {
        public ItemUpdatedNotification(Guid itemId, DateTime updatedOn) : base(itemId, updatedOn)
        {
        }
    }
}