﻿using System;
using System.Threading.Tasks;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Events.TodoItems.CancelItem;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoItems.Specifications;

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

        public virtual async Task CancelItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            await _workflowService.Process(new BeforeItemCancelledWorkflow(item.ItemId));

            item.CancelItem();

            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemCancelledWorkflow(item.ItemId, item.CancelledOn.Value));
            var notifcation = _notificationService.Queue(new ItemCancelledNotification(item.ItemId, item.CancelledOn.Value));

            await Task.WhenAll(notifcation, workflow);
        }
    }
}