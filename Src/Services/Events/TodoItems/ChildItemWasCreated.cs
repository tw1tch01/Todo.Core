using System;
using MediatR;

namespace Todo.Services.Events.TodoItems
{
    public class ChildItemWasCreated : INotification
    {
        public ChildItemWasCreated(Guid parentItemId, Guid childItemId)
        {
            ParentItemId = parentItemId;
            ChildItemId = childItemId;
        }

        public Guid ParentItemId { get; }
        public Guid ChildItemId { get; }
    }
}