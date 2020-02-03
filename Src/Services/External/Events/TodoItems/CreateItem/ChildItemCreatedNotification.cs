using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CreateItem;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class ChildItemCreatedNotification : ChildItemCreated, INotification
    {
        public ChildItemCreatedNotification(Guid parentItemId, Guid childItemId) : base(parentItemId, childItemId)
        {
        }
    }
}