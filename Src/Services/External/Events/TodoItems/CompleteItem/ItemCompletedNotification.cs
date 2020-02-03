using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CompleteItem;

namespace Todo.Services.External.Events.TodoItems.CompleteItem
{
    public class ItemCompletedNotification : ItemCompleted, INotification
    {
        public ItemCompletedNotification(Guid itemId, DateTime cancelledOn) : base(itemId, cancelledOn)
        {
        }
    }
}