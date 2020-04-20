using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoItems;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.TodoItems.Commands.UpdateItem
{
    public interface IUpdateItemService
    {
        Task<ItemValidationResult> UpdateItem(Guid itemId, UpdateItemDto itemDto);
    }
}