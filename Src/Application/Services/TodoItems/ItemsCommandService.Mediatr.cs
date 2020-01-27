using System;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Interfaces.TodoItems;
using Todo.Application.TodoItems.Commands.Actions;
using Todo.Application.TodoItems.Commands.Create;
using Todo.Application.TodoItems.Commands.Delete;
using Todo.Application.TodoItems.Commands.Update;
using Todo.Models.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    public class ItemsCommandService : IItemsCommandService
    {
        private readonly IMediator _mediator;

        public ItemsCommandService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Guid> AddChildItem(Guid parentId, CreateItemDto childItemDto)
        {
            return await _mediator.Send(new AddChildItemRequest(parentId, childItemDto));
        }

        public async Task CancelItem(Guid itemId)
        {
            await _mediator.Send(new CancelItemRequest(itemId));
        }

        public async Task CompleteItem(Guid itemId)
        {
            await _mediator.Send(new CompleteItemRequest(itemId));
        }

        public async Task<Guid> CreateItem(CreateItemDto itemDto)
        {
            return await _mediator.Send(new CreateItemRequest(itemDto));
        }

        public async Task DeleteItem(Guid itemId)
        {
            await _mediator.Send(new DeleteItemRequest(itemId));
        }

        public async Task ResetItem(Guid itemId)
        {
            await _mediator.Send(new ResetItemRequest(itemId));
        }

        public async Task StartItem(Guid itemId)
        {
            await _mediator.Send(new StartItemRequest(itemId));
        }

        public async Task UpdateItem(Guid itemId, UpdateItemDto itemDto)
        {
            await _mediator.Send(new UpdateItemRequest(itemId, itemDto));
        }
    }
}