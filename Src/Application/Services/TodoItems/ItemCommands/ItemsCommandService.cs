using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoItems;
using Todo.Services.TodoItems.Commands.Actions.CancelItem;
using Todo.Services.TodoItems.Commands.Actions.CompleteItem;
using Todo.Services.TodoItems.Commands.Actions.ResetItem;
using Todo.Services.TodoItems.Commands.Actions.StartItem;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Commands.DeleteItem;
using Todo.Services.TodoItems.Commands.UpdateItem;

namespace Todo.Application.Services.TodoItems.ItemCommands
{
    public class ItemsCommandService : IItemsCommandService
    {
        private readonly ICreateItemService _createItemService;
        private readonly IDeleteItemService _deleteItemService;
        private readonly IUpdateItemService _updateItemService;
        private readonly ICancelItemService _cancelItemService;
        private readonly ICompleteItemService _completeItemService;
        private readonly IResetItemService _resetItemService;
        private readonly IStartItemService _startItemService;

        public ItemsCommandService
        (
            ICreateItemService createItemService,
            IDeleteItemService deleteItemService,
            IUpdateItemService updateItemService,
            ICancelItemService cancelItemService,
            ICompleteItemService completeItemService,
            IResetItemService resetItemService,
            IStartItemService startItemService
        )
        {
            _createItemService = createItemService;
            _deleteItemService = deleteItemService;
            _updateItemService = updateItemService;
            _cancelItemService = cancelItemService;
            _completeItemService = completeItemService;
            _resetItemService = resetItemService;
            _startItemService = startItemService;
        }

        public async Task<Guid> AddChildItem(Guid parentId, CreateItemDto childItemDto)
        {
            return await _createItemService.AddChildItem(parentId, childItemDto);
        }

        public async Task CancelItem(Guid itemId)
        {
            await _cancelItemService.CancelItem(itemId);
        }

        public async Task CompleteItem(Guid itemId)
        {
            await _completeItemService.CompleteItem(itemId);
        }

        public async Task<Guid> CreateItem(CreateItemDto itemDto)
        {
            return await _createItemService.CreateItem(itemDto);
        }

        public async Task DeleteItem(Guid itemId)
        {
            await _deleteItemService.DeleteItem(itemId);
        }

        public async Task ResetItem(Guid itemId)
        {
            await _resetItemService.ResetItem(itemId);
        }

        public async Task StartItem(Guid itemId)
        {
            await _startItemService.StartItem(itemId);
        }

        public async Task UpdateItem(Guid itemId, UpdateItemDto itemDto)
        {
            await _updateItemService.UpdateItem(itemId, itemDto);
        }
    }
}