using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Events.CompleteItem;
using Todo.Services.Notifications;
using Todo.Services.Workflows;
using Todo.Services.TodoItems.Specifications;

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

        public virtual async Task CompleteItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            await _workflowService.Process(new BeforeItemCompletedProcess(item.ItemId));

            item.CompleteItem();

            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemCompletedProcess(item.ItemId, item.CompletedOn.Value));
            var notification = _notificationService.Queue(new ItemCompletedNotification(item.ItemId, item.CompletedOn.Value));

            await Task.WhenAll(notification, workflow);
        }
    }
}