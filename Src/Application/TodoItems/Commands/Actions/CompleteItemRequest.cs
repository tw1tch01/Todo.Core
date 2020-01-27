using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;

namespace Todo.Application.TodoItems.Commands.Actions
{
    public class CompleteItemRequest : IRequest
    {
        public CompleteItemRequest(Guid itemId)
        {
            Specification = new GetItemById(itemId);
        }

        internal GetItemById Specification { get; }

        internal class RequestHandler : IRequestHandler<CompleteItemRequest>
        {
            private readonly IContextRepository<ITodoContext> _repository;

            public RequestHandler(IContextRepository<ITodoContext> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(CompleteItemRequest request, CancellationToken cancellationToken)
            {
                var item = await _repository.GetAsync(request.Specification);

                if (item == null) throw new NotFoundException(nameof(TodoItem), request.Specification.ItemId);

                item.CompleteItem();

                return Unit.Value;
            }
        }
    }
}