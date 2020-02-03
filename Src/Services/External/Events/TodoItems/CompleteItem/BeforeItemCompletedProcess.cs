using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CompleteItem;

namespace Todo.Services.External.Events.TodoItems.CompleteItem
{
    public class BeforeItemCompletedProcess : BeforeItemCompleted, INotification
    {
        public BeforeItemCompletedProcess(Guid itemId) : base(itemId)
        {
        }
    }
}