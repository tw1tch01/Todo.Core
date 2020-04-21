using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Events.DeleteItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Commands.DeleteItem
{
    public class DeleteItemService : IDeleteItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public DeleteItemService(IContextRepository<ITodoContext> repository, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public virtual async Task<ItemValidationResult> DeleteItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) return new ItemNotFoundResult(itemId);

            await _workflowService.Process(new BeforeItemDeletedProcess(itemId));

            _repository.Remove(item);
            await _repository.SaveAsync();

            var deletedOn = DateTime.UtcNow;

            var workflow = _workflowService.Process(new ItemDeletedProcess(itemId, deletedOn));
            var notification = _notificationService.Queue(new ItemDeletedNotification(itemId, deletedOn));

            await Task.WhenAll(notification, workflow);

            return new ItemDeletedResult(itemId, deletedOn);
        }
    }
}