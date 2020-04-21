using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using FluentValidation.Results;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.TodoItems.Validators;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Events.UpdateItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

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

        public virtual async Task<ItemValidationResult> UpdateItem(Guid itemId, UpdateItemDto itemDto)
        {
            if (itemDto == null) throw new ArgumentNullException(nameof(itemDto));

            var validationResult = ValidateDto(itemDto);

            if (!validationResult.IsValid) return new InvalidDtoResult(validationResult.Errors);

            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) return new ItemNotFoundResult(itemId);

            await _workflowService.Process(new BeforeItemUpdatedProcess(item.ItemId));

            _mapper.Map(itemDto, item);
            await _repository.SaveAsync();

            var workflow = _workflowService.Process(new ItemUpdatedProcess(item.ItemId, item.ModifiedOn.Value));
            var notification = _notificationService.Queue(new ItemUpdatedNotification(item.ItemId, item.ModifiedOn.Value));

            await Task.WhenAll(notification, workflow);

            return new ItemUpdatedResult(item.ItemId, item.ModifiedOn.Value);
        }

        #region Private Methods

        private ValidationResult ValidateDto(UpdateItemDto itemDto)
        {
            var validator = new UpdateItemValidator();
            var result = validator.Validate(itemDto);
            return result;
        }

        #endregion Private Methods
    }
}