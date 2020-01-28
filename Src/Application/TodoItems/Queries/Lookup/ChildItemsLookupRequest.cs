using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Specifications;
using Todo.DomainModels.TodoItems;

[assembly: InternalsVisibleTo("Todo.Application.UnitTests")]

namespace Todo.Application.TodoItems.Queries.Lookup
{
    internal class ChildItemsLookupRequest : IRequest<ICollection<TodoItemLookup>>
    {
        public ChildItemsLookupRequest(Guid parentId)
        {
            Specification = new GetItemsByParentId(parentId);
        }

        internal GetItemsByParentId Specification { get; }

        internal class RequestHandler : IRequestHandler<ChildItemsLookupRequest, ICollection<TodoItemLookup>>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<ICollection<TodoItemLookup>> Handle(ChildItemsLookupRequest request, CancellationToken cancellationToken)
            {
                var items = await _repository.ListAsync(request.Specification);

                var details = _mapper.Map<ICollection<TodoItemLookup>>(items);

                return details;
            }
        }
    }
}