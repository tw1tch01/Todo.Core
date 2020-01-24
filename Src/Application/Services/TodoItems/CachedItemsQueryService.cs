using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.Models.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    //TODO: Implement a caching system
    public class CachedItemsQueryService : AbstractItemsQueryService
    {
        private readonly AbstractItemsQueryService _queryService;

        public CachedItemsQueryService(AbstractItemsQueryService queryService)
        {
            _queryService = queryService;
        }

        public override async Task<TodoItemDetails> GetItem(Guid guid)
        {
            // Caching checks done here

            return await _queryService.GetItem(guid);
        }

        public override async Task<ICollection<TodoItemDetails>> GetChildItems(Guid parentId)
        {
            // Caching checks done here

            return await _queryService.GetChildItems(parentId);
        }

        public override async Task<ICollection<ParentTodoItemLookup>> ListItems(TodoItemLookupParams parameters)
        {
            // Caching checks done here

            return await _queryService.ListItems(parameters);
        }

        public override async Task<PagedCollection<ParentTodoItemLookup>> PagedListItems(int page, int pageSize, TodoItemLookupParams parameters)
        {
            // Caching checks done here

            return await _queryService.PagedListItems(page, pageSize, parameters);
        }
    }
}