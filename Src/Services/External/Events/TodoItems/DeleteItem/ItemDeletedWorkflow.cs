using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.DeleteItem;

namespace Todo.Services.External.Events.TodoItems.DeleteItem
{
    public class ItemDeletedWorkflow : ItemDeleted, INotification
    {
        public ItemDeletedWorkflow(Guid itemId, DateTime deletedOn) : base(itemId, deletedOn)
        {
        }
    }
}