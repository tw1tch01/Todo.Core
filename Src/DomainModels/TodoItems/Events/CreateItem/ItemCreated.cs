using System;
using Todo.DomainModels.Common;

namespace Todo.DomainModels.TodoItems.Events.CreateItem
{
    public class ItemCreated : IWorkflowProcess, INotificationProcess
    {
        public ItemCreated(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}