using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.DeleteItem;

namespace Todo.Services.External.Events.TodoItems.DeleteItem
{
    public class ItemDeletedNotification : ItemDeleted, INotification
    {
        public ItemDeletedNotification(Guid itemId, DateTime deletedOn) : base(itemId, deletedOn)
        {
        }
    }
}