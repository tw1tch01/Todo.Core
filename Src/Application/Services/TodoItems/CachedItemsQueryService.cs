using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.Application.Common;
using Todo.Application.Interfaces;
using Todo.Application.Interfaces.TodoItems;
using Todo.DomainModels.TodoItems;

namespace Todo.Application.Services.TodoItems
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

        public async Task<ICollection<TodoItemLookup>> GetChildItems(Guid parentId)
        {
            return await _cacheService.Get(CacheKeys.Items.GetChildItems(parentId), CacheKeys.Times.Default, async () =>
            {
                return await _queryService.GetChildItems(parentId);
            });
        }

        public async Task<ICollection<ParentTodoItemLookup>> LookupItems(TodoItemLookupParams parameters)
        {
            return await _cacheService.Get(CacheKeys.Items.LookupItems(parameters), CacheKeys.Times.Default, async () =>
            {
                return await _queryService.LookupItems(parameters);
            });
        }

        public async Task<PagedCollection<ParentTodoItemLookup>> PagedLookupItems(int page, int pageSize, TodoItemLookupParams parameters)
        {
            return await _cacheService.Get(CacheKeys.Items.PagedLookupItems(page, pageSize, parameters), CacheKeys.Times.Default, async () =>
            {
                return await _queryService.PagedLookupItems(page, pageSize, parameters);
            });
        }
    }
}