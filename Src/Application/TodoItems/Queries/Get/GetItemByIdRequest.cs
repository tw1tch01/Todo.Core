using System;
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
                request.Specification.Include(a => a.ChildItems);

                var item = await _repository.GetAsync(request.Specification);

                if (item == null) throw new NotFoundException(nameof(TodoItem), request.Specification.ItemId);

                var details = _mapper.Map<TodoItemDetails>(item);

                return details;
            }
        }
    }
}