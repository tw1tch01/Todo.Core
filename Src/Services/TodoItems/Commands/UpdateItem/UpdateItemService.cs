using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Events.TodoItems.UpdateItem;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Commands.UpdateItem
{
    public class UpdateItemService : IUpdateItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public UpdateItemService(IContextRepository<ITodoContext> repository, IMapper mapper, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public virtual async Task UpdateItem(Guid itemId, UpdateItemDto itemDto)
        {
            if (itemDto == null) throw new ArgumentNullException(nameof(itemDto));

            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            await _workflowService.Process(new BeforeItemUpdatedProcess(item.ItemId));

            _mapper.Map(itemDto, item);
            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemUpdatedProcess(item.ItemId, item.ModifiedOn.Value));
            var notification = _notificationService.Queue(new ItemUpdatedNotification(item.ItemId, item.ModifiedOn.Value));

            await Task.WhenAll(notification, workflow);
        }
    }
}