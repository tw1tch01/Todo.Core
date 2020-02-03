using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CreateItem;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class ChildItemCreatedWorkflow : ChildItemCreated, INotification
    {
        public ChildItemCreatedWorkflow(Guid parentItemId, Guid childItemId) : base(parentItemId, childItemId)
        {
        }
    }
}