using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.TodoItems.Commands.Delete
{
    internal class DeleteItemRequest : IRequest
    {
        public DeleteItemRequest(Guid itemId)
        {
            ItemId = itemId;
        }

        internal Guid ItemId { get; }

        internal class RequestHandler : IRequestHandler<DeleteItemRequest>
        {
            private readonly IContextRepository<ITodoContext> _repository;

            public RequestHandler(IContextRepository<ITodoContext> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteItemRequest request, CancellationToken cancellationToken)
            {
                var item = await _repository.FindByPrimaryKeyAsync<TodoItem, Guid>(request.ItemId);

                if (item != null)
                {
                    _repository.Remove(item);
                    await _repository.SaveAsync();
                }

                return Unit.Value;
            }
        }
    }
}