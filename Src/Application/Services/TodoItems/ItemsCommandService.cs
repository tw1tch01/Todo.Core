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

        public Task<Guid> AddChildItem(Guid parentId, CreateItemDto childItemDto) => _createItemService.AddChildItem(parentId, childItemDto);

        public Task CancelItem(Guid itemId) => _cancelItemService.CancelItem(itemId);

        public Task CompleteItem(Guid itemId) => _completeItemService.CompleteItem(itemId);

        public Task<Guid> CreateItem(CreateItemDto itemDto) => _createItemService.CreateItem(itemDto);

        public Task DeleteItem(Guid itemId) => _deleteItemService.DeleteItem(itemId);

        public Task ResetItem(Guid itemId) => _resetItemService.ResetItem(itemId);

        public Task StartItem(Guid itemId) => _startItemService.StartItem(itemId);

        public Task UpdateItem(Guid itemId, UpdateItemDto itemDto) => _updateItemService.UpdateItem(itemId, itemDto);
    }
}