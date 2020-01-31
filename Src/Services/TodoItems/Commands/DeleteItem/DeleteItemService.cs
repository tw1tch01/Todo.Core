using System;
using System.Threading.Tasks;
using Data.Repositories;
using MediatR;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.Events.TodoItems;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Commands.DeleteItem
{
    internal class DeleteItemService : IDeleteItemService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMediator _mediator;

        public DeleteItemService(IContextRepository<ITodoContext> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task DeleteItem(Guid itemId)
        {
            var item = await _repository.GetAsync(new GetItemById(itemId));

            if (item == null) throw new NotFoundException(nameof(TodoItem), itemId);

            _repository.Remove(item);
            await _repository.SaveAsync();

            await _mediator.Publish(new ItemWasDeleted(itemId, DateTime.UtcNow));
        }
    }
}