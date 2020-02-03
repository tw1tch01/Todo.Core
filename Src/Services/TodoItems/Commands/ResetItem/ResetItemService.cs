using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Events.TodoItems.ResetItem;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Commands.ResetItem
{
    public class ResetItemService : IResetItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public ResetItemService(IContextRepository<ITodoContext> repository, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public virtual async Task ResetItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            await _workflowService.Process(new BeforeItemResetProcess(item.ItemId));

            item.ResetItem();

            await _repository.SaveAsync();

            var resetOn = DateTime.UtcNow;
            var workflow = _workflowService.Process(new ItemResetProcess(item.ItemId, resetOn));
            var notification = _notificationService.Queue(new ItemResetNotification(item.ItemId, resetOn));

            await Task.WhenAll(notification, workflow);
        }
    }
}