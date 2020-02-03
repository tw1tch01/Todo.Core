using System;
using Todo.DomainModels.TodoItems.Events.DeleteItem;

namespace Todo.Services.External.Events.TodoItems.DeleteItem
{
    public class BeforeItemDeletedWorkflow : BeforeItemDeleted
    {
        public BeforeItemDeletedWorkflow(Guid itemId) : base(itemId)
        {
        }
    }
}