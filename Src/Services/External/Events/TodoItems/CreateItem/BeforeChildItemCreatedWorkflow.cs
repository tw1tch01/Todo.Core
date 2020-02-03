using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CreateItem;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class BeforeChildItemCreatedWorkflow : BeforeChildItemCreated, INotification
    {
        public BeforeChildItemCreatedWorkflow(Guid parentItemId) : base(parentItemId)
        {
        }
    }
}