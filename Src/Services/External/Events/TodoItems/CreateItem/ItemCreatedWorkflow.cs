using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CreateItem;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class ItemCreatedWorkflow : ItemCreated, INotification
    {
        public ItemCreatedWorkflow(Guid itemId) : base(itemId)
        {
        }
    }
}