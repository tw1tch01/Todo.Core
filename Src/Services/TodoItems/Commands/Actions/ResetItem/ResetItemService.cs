using System;
using System.Threading.Tasks;
using Data.Repositories;
using MediatR;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.Events.TodoItems;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Commands.Actions.ResetItem
{
    internal class ResetItemService : IResetItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMediator _mediator;

        public ResetItemService(IContextRepository<ITodoContext> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task ResetItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            item.ResetItem();

            await _mediator.Publish(new ItemWasReset(item.ItemId));
        }
    }
}