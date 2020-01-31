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

namespace Todo.Services.TodoItems.Commands.UpdateItem
{
    internal class UpdateItemService : IUpdateItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateItemService(IContextRepository<ITodoContext> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task UpdateItem(Guid itemId, UpdateItemDto itemDto)
        {
            if (itemDto == null) throw new ArgumentNullException(nameof(itemDto));

            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            _mapper.Map(itemDto, item);
            await _repository.SaveAsync();

            await _mediator.Publish(new ItemWasUpdated(item.ItemId));
        }
    }
}