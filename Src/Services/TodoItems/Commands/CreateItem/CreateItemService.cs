using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using FluentValidation.Results;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.TodoItems.Validators;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Events.CreateItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Commands.CreateItem
{
    public class CreateItemService : ICreateItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IWorkflowService _workflowService;

        public CreateItemService(IContextRepository<ITodoContext> repository, IMapper mapper, INotificationService notificationService, IWorkflowService workflowService)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationService = notificationService;
            _workflowService = workflowService;
        }

        public virtual async Task<ItemValidationResult> CreateItem(CreateItemDto itemDto)
        {
            if (itemDto == null) throw new ArgumentNullException(nameof(itemDto));

            var validationResult = ValidateDto(itemDto);

            if (!validationResult.IsValid) return new InvalidDtoResult(validationResult.Errors);

            var item = _mapper.Map<TodoItem>(itemDto);

            await _repository.AddAsync(item);
            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemCreatedProcess(item.ItemId));
            var notification = _notificationService.Queue(new ItemCreatedNotification(item.ItemId));

            await Task.WhenAll(notification, workflow);

            return new ItemCreatedResult(item.ItemId, item.CreatedOn);
        }

        public virtual async Task<ItemValidationResult> AddChildItem(Guid parentItemId, CreateItemDto childItemDto)
        {
            if (childItemDto == null) throw new ArgumentNullException(nameof(childItemDto));

            var validationResult = ValidateDto(childItemDto);

            if (!validationResult.IsValid) return new InvalidDtoResult(validationResult.Errors);

            var parentItem = await _repository.GetAsync(new GetItemById(parentItemId));

            if (parentItem == null) return new ItemNotFoundResult(parentItemId);

            await _workflowService.Process(new BeforeChildItemCreatedProcess(parentItem.ItemId));

            var childItem = _mapper.Map<TodoItem>(childItemDto);

            parentItem.ChildItems.Add(childItem);
            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ChildItemCreatedProcess(parentItem.ItemId, childItem.ItemId));
            var notification = _notificationService.Queue(new ChildItemCreatedNotification(parentItem.ItemId, childItem.ItemId));

            await Task.WhenAll(notification, workflow);

            return new ItemCreatedResult(childItem.ItemId, childItem.CreatedOn);
        }

        #region Private Methods

        private ValidationResult ValidateDto(CreateItemDto itemDto)
        {
            var validator = new CreateItemValidator();
            var result = validator.Validate(itemDto);
            return result;
        }

        #endregion Private Methods
    }
}