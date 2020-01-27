using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

[assembly: InternalsVisibleTo("Todo.Application.IntegrationTests")]
[assembly: InternalsVisibleTo("Todo.Application.UnitTests")]

namespace Todo.Application.TodoItems.Queries.Get
{
    internal class GetItemByIdRequest : IRequest<TodoItemDetails>
    {
        public GetItemByIdRequest(Guid itemId)
        {
            Specification = new GetItemById(itemId);
        }

        internal GetItemById Specification { get; }

        internal class RequestHandler : IRequestHandler<GetItemByIdRequest, TodoItemDetails>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<TodoItemDetails> Handle(GetItemByIdRequest request, CancellationToken cancellationToken)
            {
                var item = await _repository.GetAsync(request.Specification);

                if (item == null) throw new NotFoundException(nameof(TodoItem), request.Specification.ItemId);

                var getChildItemsTask = _repository.ListAsync(new GetItemsByParentId(item.ItemId));

                // TODO: get notes:
                //var getNotesTask = _repository.ListAsync(new GetNotesByItemId(item.ItemId));

                await Task.WhenAll(getChildItemsTask/*, getNotesTask*/);

                item.ChildItems.ToList().AddRange(await getChildItemsTask);
                //item.Notes.ToList().AddRange(await getNotesTask);

                var details = _mapper.Map<TodoItemDetails>(item);
                return details;
            }
        }
    }
}