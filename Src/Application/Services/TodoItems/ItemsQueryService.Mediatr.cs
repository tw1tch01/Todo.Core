using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using MediatR;
using Todo.Application.Interfaces.TodoItems;
using Todo.Application.TodoItems.Queries.Get;
using Todo.Application.TodoItems.Queries.Lookup;
using Todo.Models.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    public class ItemsQueryService : IItemsQueryService
    {
        private readonly IMediator _mediator;

        public ItemsQueryService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TodoItemDetails> GetItem(Guid guid)
        {
            return await _mediator.Send(new GetItemByIdRequest(guid));
        }

        public async Task<ICollection<TodoItemLookup>> GetChildItems(Guid parentId)
        {
            return await _mediator.Send(new ChildItemsLookupRequest(parentId));
        }

        public async Task<ICollection<ParentTodoItemLookup>> LookupItems(TodoItemLookupParams parameters)
        {
            var request = new ParentItemsLookup();

            request.WithFilters(parameters);

            return await _mediator.Send(request);
        }

        public async Task<PagedCollection<ParentTodoItemLookup>> PagedLookupItems(int page, int pageSize, TodoItemLookupParams parameters)
        {
            var request = new PagedParentItemsLookup(page, pageSize);

            request.WithFilters(parameters);

            return await _mediator.Send(request);
        }
    }
}