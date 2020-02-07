using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.Application.Services.TodoItems.ItemQueries;
using Todo.DomainModels.TodoItems;
using Todo.Services.Cache;

namespace Todo.Application.Services.TodoItems.CachedItemQueries
{
    public class CachedItemsQueryService : ICachedItemsQueryService
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
            return await _cacheService.Get(CacheKeys.Items.GetItem(parentId), CacheKeys.Times.Default, async () =>
            {
                return await _queryService.GetItem(parentId);
            });
        }

        public async Task<ICollection<TodoItemLookup>> LookupChildItems(Guid parentId)
        {
            return await _cacheService.Get(CacheKeys.Items.Lookups.ChildItems(parentId), CacheKeys.Times.Default, async () =>
            {
                return await _queryService.LookupChildItems(parentId);
            });
        }

        public async Task<ICollection<ParentTodoItemLookup>> LookupParentItems(TodoItemLookupParams parameters)
        {
            return await _cacheService.Get(CacheKeys.Items.Lookups.ParentItems(parameters), CacheKeys.Times.Default, async () =>
            {
                return await _queryService.LookupParentItems(parameters);
            });
        }

        public async Task<PagedCollection<ParentTodoItemLookup>> PagedLookupParentItems(int page, int pageSize, TodoItemLookupParams parameters)
        {
            return await _cacheService.Get(CacheKeys.Items.Lookups.PagedParentItems(page, pageSize, parameters), CacheKeys.Times.Default, async () =>
            {
                return await _queryService.PagedLookupParentItems(page, pageSize, parameters);
            });
        }
    }
}