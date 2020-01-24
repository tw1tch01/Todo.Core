using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using MediatR;
using Todo.Application.TodoItems.Queries.Get;
using Todo.Application.TodoItems.Queries.Lookup;
using Todo.Models.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    public class ItemsQueryService : AbstractItemsQueryService
    {
        private readonly IMediator _mediator;

        public ItemsQueryService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<TodoItemDetails> GetItem(Guid guid)
        {
            return await _mediator.Send(new GetItemById(guid));
        }

        public override async Task<ICollection<TodoItemDetails>> GetChildItems(Guid parentId)
        {
            return await _mediator.Send(new GetItemsByParentId(parentId));
        }

        public override async Task<ICollection<ParentTodoItemLookup>> ListItems(TodoItemLookupParams parameters)
        {
            var request = new ParentItemsLookup();

            request.WithFilters(parameters);

            return await _mediator.Send(request);
        }

        public override async Task<PagedCollection<ParentTodoItemLookup>> PagedListItems(int page, int pageSize, TodoItemLookupParams parameters)
        {
            var request = new PagedParentItemsLookup(page, pageSize);

            request.WithFilters(parameters);

            return await _mediator.Send(request);
        }
    }
}