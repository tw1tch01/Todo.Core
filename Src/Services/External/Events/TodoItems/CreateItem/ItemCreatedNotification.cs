using System;
using MediatR;
using Todo.DomainModels.TodoItems.Events.CreateItem;

namespace Todo.Services.External.Events.TodoItems.CreateItem
{
    public class ItemCreatedNotification : ItemCreated, INotification
    {
        public ItemCreatedNotification(Guid itemId) : base(itemId)
        {
        }
    }
}