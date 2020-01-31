using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoItems;

namespace Todo.Services.TodoItems.Commands.UpdateItem
{
    public interface IUpdateItemService
    {
        Task UpdateItem(Guid itemId, UpdateItemDto itemDto);
    }
}