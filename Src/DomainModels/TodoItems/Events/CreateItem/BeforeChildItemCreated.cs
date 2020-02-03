using System;

namespace Todo.DomainModels.TodoItems.Events.CreateItem
{
    public class BeforeChildItemCreated
    {
        public BeforeChildItemCreated(Guid parentItemId)
        {
            ParentItemId = parentItemId;
        }

        public Guid ParentItemId { get; }
    }
}