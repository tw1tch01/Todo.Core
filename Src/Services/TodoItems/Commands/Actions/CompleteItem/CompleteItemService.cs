using System;
using System.Threading.Tasks;
using Data.Repositories;
using MediatR;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.Events.TodoItems;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Commands.Actions.CompleteItem
{
    internal class CompleteItemService : ICompleteItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMediator _mediator;

        public CompleteItemService(IContextRepository<ITodoContext> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task CompleteItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            item.CompleteItem();

            await _mediator.Publish(new ItemWasCompleted(item.ItemId, item.CompletedOn.Value));
        }
    }
}