using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoItems;

namespace Todo.Services.TodoItems.Queries.GetItem
{
    public interface IGetItemService
    {
        Task<TodoItemDetails> GetItem(Guid itemId);
    }
}