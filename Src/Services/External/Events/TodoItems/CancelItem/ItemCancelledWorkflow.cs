using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CancelItem;

namespace Todo.Services.External.Events.TodoItems.CancelItem
{
    public class ItemCancelledWorkflow : ItemCancelled, INotification
    {
        public ItemCancelledWorkflow(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}