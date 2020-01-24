using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.Models.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    public interface IItemsQueryService
    {
        Task<TodoItemDetails> GetItem(Guid guid);

        Task<ICollection<TodoItemDetails>> GetChildItems(Guid parentId);

        Task<ICollection<ParentTodoItemLookup>> ListItems(TodoItemLookupParams parameters);

        Task<PagedCollection<ParentTodoItemLookup>> PagedListItems(int page, int pageSize, TodoItemLookupParams parameters);
    }
}