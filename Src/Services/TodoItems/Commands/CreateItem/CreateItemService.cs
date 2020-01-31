using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.Events.TodoItems;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Commands.CreateItem
{
    internal class CreateItemService : ICreateItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateItemService(IContextRepository<ITodoContext> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> AddChildItem(Guid parentItemId, CreateItemDto childItemDto)
        {
            if (childItemDto == null) throw new ArgumentNullException(nameof(childItemDto));

            var parentItem = await _repository.GetAsync(new GetItemById(parentItemId));

            if (parentItem == null) throw new NotFoundException(nameof(TodoItem), parentItemId);

            var childItem = _mapper.Map<TodoItem>(childItemDto);

            parentItem.ChildItems.Add(childItem);
            await _repository.SaveAsync();

            await _mediator.Publish(new ChildItemWasCreated(parentItemId, childItem.ItemId));

            return childItem.ItemId;
        }

        public async Task<Guid> CreateItem(CreateItemDto itemDto)
        {
            if (itemDto == null) throw new ArgumentNullException(nameof(itemDto));

            var item = _mapper.Map<TodoItem>(itemDto);

            await _repository.AddAsync(item);
            await _repository.SaveAsync();

            await _mediator.Publish(new ItemWasCreated(item.ItemId));

            return item.ItemId;
        }
    }
}