using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Events.StartItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Commands.StartItem
{
    public class StartItemService : IStartItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public StartItemService(IContextRepository<ITodoContext> repository, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public virtual async Task<ItemValidationResult> StartItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) return ItemValidationResultFactory.ItemNotFound(itemId);

            if (item.IsCancelled()) return ItemValidationResultFactory.ItemPreviouslyCancelled(item.ItemId, item.CancelledOn.Value);

            if (item.IsCompleted()) return ItemValidationResultFactory.ItemPreviouslyCompleted(item.ItemId, item.CompletedOn.Value);

            if (item.HasStarted()) return ItemValidationResultFactory.ItemAlreadyStarted(item.ItemId, item.StartedOn.Value);

            await _workflowService.Process(new BeforeItemStartedProcess(item.ItemId));

            item.StartItem();

            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemStartedProcess(item.ItemId, item.StartedOn.Value));
            var notification = _notificationService.Queue(new ItemStartedNotification(item.ItemId, item.StartedOn.Value));

            await Task.WhenAll(notification, workflow);

            return ItemValidationResultFactory.ItemStarted(item.ItemId, item.StartedOn.Value);
        }
    }
}