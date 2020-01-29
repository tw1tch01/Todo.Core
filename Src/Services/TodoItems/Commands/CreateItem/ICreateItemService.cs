using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoItems;

namespace Todo.Services.TodoItems.Commands.CreateItem
{
    public interface ICreateItemService
    {
        Task<Guid> AddChildItem(Guid parentItemId, CreateItemDto childItemDto);

        Task<Guid> CreateItem(CreateItemDto itemDto);
    }
}