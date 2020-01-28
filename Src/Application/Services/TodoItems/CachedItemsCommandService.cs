using System;
using System.Threading.Tasks;
using Todo.Application.Common;
using Todo.Application.Interfaces;
using Todo.Application.Interfaces.TodoItems;
using Todo.DomainModels.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    public class CachedItemsCommandService : ICachedCommandsQueryService
    {
        private readonly IItemsCommandService _commandService;
        private readonly ICacheService _cacheService;

        public CachedItemsCommandService(IItemsCommandService commandService, ICacheService cacheService)
        {
            _commandService = commandService;
            _cacheService = cacheService;
        }

        public async Task<Guid> AddChildItem(Guid parentId, CreateItemDto childItemDto)
        {
            var childItemId = await _commandService.AddChildItem(parentId, childItemDto);
            await ClearCachedItem(parentId);
            return childItemId;
        }

        public async Task CancelItem(Guid itemId)
        {
            await _commandService.CancelItem(itemId);
            await ClearCachedItem(itemId);
        }

        public async Task CompleteItem(Guid itemId)
        {
            await _commandService.CancelItem(itemId);
            await ClearCachedItem(itemId);
        }

        public async Task<Guid> CreateItem(CreateItemDto itemDto)
        {
            return await _commandService.CreateItem(itemDto);
        }

        public async Task DeleteItem(Guid itemId)
        {
            await _commandService.DeleteItem(itemId);
            await ClearCachedItem(itemId);
        }

        public async Task ResetItem(Guid itemId)
        {
            await _commandService.ResetItem(itemId);
            await ClearCachedItem(itemId);
        }

        public async Task StartItem(Guid itemId)
        {
            await _commandService.StartItem(itemId);
            await ClearCachedItem(itemId);
        }

        public async Task UpdateItem(Guid itemId, UpdateItemDto itemDto)
        {
            await _commandService.UpdateItem(itemId, itemDto);
            await ClearCachedItem(itemId);
        }

        private async Task ClearCachedItem(Guid parentId)
        {
            if (await _cacheService.Contains(CacheKeys.Items.GetItem(parentId)))
            {
                await _cacheService.Remove(CacheKeys.Items.GetItem(parentId));
            }
        }
    }
}