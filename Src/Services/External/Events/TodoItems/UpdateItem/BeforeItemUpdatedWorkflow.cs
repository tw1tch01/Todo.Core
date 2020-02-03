using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.UpdateItem;

namespace Todo.Services.External.Events.TodoItems.UpdateItem
{
    public class BeforeItemUpdatedWorkflow : BeforeItemUpdated, INotification
    {
        public BeforeItemUpdatedWorkflow(Guid itemId) : base(itemId)
        {
        }
    }
}