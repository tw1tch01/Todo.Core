using System;
using System.Threading.Tasks;
using Todo.DomainModels.TodoItems;
using Todo.Services.TodoItems.Commands.CancelItem;
using Todo.Services.TodoItems.Commands.CompleteItem;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Commands.DeleteItem;
using Todo.Services.TodoItems.Commands.ResetItem;
using Todo.Services.TodoItems.Commands.StartItem;
using Todo.Services.TodoItems.Commands.UpdateItem;
using Todo.Services.TodoItems.Validation;

namespace Todo.Application.Services.TodoItems
{
    public class ItemsCommandService : ICreateItemService, IDeleteItemService, IUpdateItemService, ICancelItemService, ICompleteItemService, IResetItemService, IStartItemService
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

        public Task<ItemValidationResult> AddChildItem(Guid parentId, CreateItemDto childItemDto) => _createItemService.AddChildItem(parentId, childItemDto);

        public Task<ItemValidationResult> DeleteItem(Guid itemId) => _deleteItemService.DeleteItem(itemId);

        public Task<ItemValidationResult> ResetItem(Guid itemId) => _resetItemService.ResetItem(itemId);

        public Task<ItemValidationResult> StartItem(Guid itemId) => _startItemService.StartItem(itemId);

        public Task<ItemValidationResult> UpdateItem(Guid itemId, UpdateItemDto itemDto) => _updateItemService.UpdateItem(itemId, itemDto);

        public Task<ItemValidationResult> CancelItem(Guid itemId) => _cancelItemService.CancelItem(itemId);

        public Task<ItemValidationResult> CompleteItem(Guid itemId) => _completeItemService.CompleteItem(itemId);

        public Task<ItemValidationResult> CreateItem(CreateItemDto itemDto) => _createItemService.CreateItem(itemDto);
    }
}