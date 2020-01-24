using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.Models.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    public abstract class AbstractItemsQueryService
    {
        public abstract Task<TodoItemDetails> GetItem(Guid guid);

        public abstract Task<ICollection<TodoItemDetails>> GetChildItems(Guid parentId);

        public abstract Task<ICollection<ParentTodoItemLookup>> ListItems(TodoItemLookupParams parameters);

        public abstract Task<PagedCollection<ParentTodoItemLookup>> PagedListItems(int page, int pageSize, TodoItemLookupParams parameters);
    }
}