using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Events.CancelItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Commands.CancelItem
{
    public class CancelItemService : ICancelItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public CancelItemService(IContextRepository<ITodoContext> repository, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public virtual async Task<ItemValidationResult> CancelItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) return new ItemNotFoundResult(itemId);

            if (item.IsCancelled()) return new ItemPreviouslyCancelledResult(item.ItemId, item.CancelledOn.Value);

            if (item.IsCompleted()) return new ItemPreviouslyCompletedResult(item.ItemId, item.CompletedOn.Value);

            await _workflowService.Process(new BeforeItemCancelledProcess(item.ItemId));

            item.CancelItem();

            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemCancelledProcess(item.ItemId, item.CancelledOn.Value));
            var notifcation = _notificationService.Queue(new ItemCancelledNotification(item.ItemId, item.CancelledOn.Value));

            await Task.WhenAll(notifcation, workflow);

            return new ItemCancelledResult(item.ItemId, item.CancelledOn.Value);
        }
    }
}