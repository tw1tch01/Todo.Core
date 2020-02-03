using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.ResetItem;

namespace Todo.Services.External.Events.TodoItems.ResetItem
{
    public class BeforeItemResetWorkflow : BeforeItemReset, INotification
    {
        public BeforeItemResetWorkflow(Guid itemId) : base(itemId)
        {
        }
    }
}