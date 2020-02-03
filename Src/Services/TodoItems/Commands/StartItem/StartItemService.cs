using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Events.TodoItems.StartItem;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoItems.Specifications;

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

        public virtual async Task StartItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            await _workflowService.Process(new BeforeItemStartedProcess(item.ItemId));

            item.StartItem();

            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemStartedProcess(item.ItemId, item.StartedOn.Value));
            var notification = _notificationService.Queue(new ItemStartedNotification(item.ItemId, item.StartedOn.Value));

            await Task.WhenAll(notification, workflow);
        }
    }
}