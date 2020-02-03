using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.StartItem;

namespace Todo.Services.External.Events.TodoItems.StartItem
{
    public class BeforeItemStartedWorkflow : BeforeItemStarted, INotification
    {
        public BeforeItemStartedWorkflow(Guid itemId) : base(itemId)
        {
        }
    }
}