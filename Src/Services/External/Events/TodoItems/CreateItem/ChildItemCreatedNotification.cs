using System;
using Todo.DomainModels.TodoItems.Events.CreateItem;
using Todo.Services.Common;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class ChildItemCreatedNotification : ChildItemCreated, INotificationProcess
    {
        public ChildItemCreatedNotification(Guid parentItemId, Guid childItemId) : base(parentItemId, childItemId)
        {
        }
    }
}