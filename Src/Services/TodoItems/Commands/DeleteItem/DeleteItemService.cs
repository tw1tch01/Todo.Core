using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Events.DeleteItem;
using Todo.Services.Notifications;
using Todo.Services.Workflows;
using Todo.Services.TodoItems.Specifications;

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

        public virtual async Task DeleteItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            await _workflowService.Process(new BeforeItemDeletedProcess(itemId));

            _repository.Remove(item);
            await _repository.SaveAsync();

            var deletedOn = DateTime.UtcNow;

            var workflow = _workflowService.Process(new ItemDeletedProcess(itemId, deletedOn));
            var notification = _notificationService.Queue(new ItemDeletedNotification(itemId, deletedOn));

            await Task.WhenAll(notification, workflow);
        }
    }
}