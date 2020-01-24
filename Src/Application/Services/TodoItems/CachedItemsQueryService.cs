using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.Application.Interfaces;
using Todo.Models.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    public class CachedItemsQueryService : IItemsQueryService
    {
        private readonly IItemsQueryService _queryService;
        private readonly ICacheService _cacheService;

        public CachedItemsQueryService(IItemsQueryService queryService, ICacheService cacheService)
        {
            _queryService = queryService;
            _cacheService = cacheService;
        }

        public async Task<TodoItemDetails> GetItem(Guid parentId)
        {
            return await _cacheService.Get(CacheKey.Items.Item(parentId), CacheKey.Times.Default, async () =>
            {
                return await _queryService.GetItem(parentId);
            });
        }

        public async Task<ICollection<TodoItemDetails>> GetChildItems(Guid parentId)
        {
            return await _cacheService.Get(CacheKey.Items.ChildItems(parentId), CacheKey.Times.Default, async () =>
            {
                return await _queryService.GetChildItems(parentId);
            });
        }

        public async Task<ICollection<ParentTodoItemLookup>> ListItems(TodoItemLookupParams parameters)
        {
            return await _cacheService.Get(CacheKey.Items.ListItems(parameters), CacheKey.Times.Default, async () =>
            {
                return await _queryService.ListItems(parameters);
            });
        }

        public async Task<PagedCollection<ParentTodoItemLookup>> PagedListItems(int page, int pageSize, TodoItemLookupParams parameters)
        {
            return await _cacheService.Get(CacheKey.Items.PagedItems(page, pageSize, parameters), CacheKey.Times.Default, async () =>
            {
                return await _queryService.PagedListItems(page, pageSize, parameters);
            });
        }
    }
}