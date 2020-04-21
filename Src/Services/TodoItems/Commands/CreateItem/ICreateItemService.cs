using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoItems;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.TodoItems.Commands.CreateItem
{
    public interface ICreateItemService
    {
        Task<ItemValidationResult> AddChildItem(Guid parentItemId, CreateItemDto childItemDto);

        Task<ItemValidationResult> CreateItem(CreateItemDto itemDto);
    }
}