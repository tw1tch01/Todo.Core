using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CompleteItem;

namespace Todo.Services.External.Events.TodoItems.CompleteItem
{
    public class ItemCompletedProcess : ItemCompleted, INotification
    {
        public ItemCompletedProcess(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}