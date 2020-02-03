using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CancelItem;

namespace Todo.Services.External.Events.TodoItems.CancelItem
{
    public class BeforeItemCancelledWorkflow : BeforeItemCancelled, INotification
    {
        public BeforeItemCancelledWorkflow(Guid itemId) : base(itemId)
        {
        }
    }
}