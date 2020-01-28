using System;
using System.Threading.Tasks;
using Todo.Models.TodoItems;

namespace Todo.Application.Interfaces.TodoItems
{
    public interface IItemsCommandService
    {
        Task<Guid> AddChildItem(Guid parentId, CreateItemDto childItemDto);

        Task CancelItem(Guid itemId);

        Task CompleteItem(Guid itemId);

        Task<Guid> CreateItem(CreateItemDto itemDto);

        Task DeleteItem(Guid itemId);

        Task ResetItem(Guid itemId);

        Task StartItem(Guid itemId);

        Task UpdateItem(Guid itemId, UpdateItemDto itemDto);
    }
}