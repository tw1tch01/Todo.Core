using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Events.CompleteItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Commands.CompleteItem
{
    public class CompleteItemService : ICompleteItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public CompleteItemService(IContextRepository<ITodoContext> repository, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public virtual async Task<ItemValidationResult> CompleteItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) return ItemValidationResultFactory.ItemNotFound(itemId);

            if (item.IsCancelled()) return ItemValidationResultFactory.ItemPreviouslyCancelled(item.ItemId, item.CancelledOn.Value);

            if (item.IsCompleted()) return ItemValidationResultFactory.ItemPreviouslyCompleted(item.ItemId, item.CompletedOn.Value);

            await _workflowService.Process(new BeforeItemCompletedProcess(item.ItemId));

            item.CompleteItem();

            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemCompletedProcess(item.ItemId, item.CompletedOn.Value));
            var notification = _notificationService.Queue(new ItemCompletedNotification(item.ItemId, item.CompletedOn.Value));

            await Task.WhenAll(notification, workflow);

            return ItemValidationResultFactory.ItemCompleted(item.ItemId, item.CompletedOn.Value);
        }
    }
}