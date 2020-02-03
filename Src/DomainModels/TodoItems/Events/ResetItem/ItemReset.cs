using System;

namespace Todo.DomainModels.TodoItems.Events.ResetItem
{
    public class ItemReset
    {
        public ItemReset(Guid itemId, DateTime resetOn)
        {
            ItemId = itemId;
            ResetOn = resetOn;
        }

        public Guid ItemId { get; }
        public DateTime ResetOn { get; }
    }
}