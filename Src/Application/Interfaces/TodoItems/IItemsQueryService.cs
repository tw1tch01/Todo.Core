using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.Models.TodoItems;

namespace Todo.Application.Interfaces.TodoItems
{
    public interface IItemsQueryService
    {
        Task<TodoItemDetails> GetItem(Guid guid);

        Task<ICollection<TodoItemLookup>> GetChildItems(Guid parentId);

        Task<ICollection<ParentTodoItemLookup>> LookupItems(TodoItemLookupParams parameters);

        Task<PagedCollection<ParentTodoItemLookup>> PagedLookupItems(int page, int pageSize, TodoItemLookupParams parameters);
    }
}