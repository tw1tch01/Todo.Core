using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Data.Specifications;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

[assembly: InternalsVisibleTo("Todo.Application.IntegrationTests")]
[assembly: InternalsVisibleTo("Todo.Application.UnitTests")]

namespace Todo.Application.TodoItems.Queries.Get
{
    internal class GetItemById : LinqSpecification<TodoItem>, IRequest<TodoItemDetails>
    {
        private readonly Guid _itemId;

        public GetItemById(Guid itemId)
        {
            _itemId = itemId;
        }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => item.ItemId == _itemId;
        }

        internal class Handler : IRequestHandler<GetItemById, TodoItemDetails>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public Handler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<TodoItemDetails> Handle(GetItemById request, CancellationToken cancellationToken)
            {
                var item = await _repository.GetAsync(request);

                if (item == null) throw new NotFoundException(nameof(TodoItem), request._itemId);

                var details = _mapper.Map<TodoItemDetails>(item);

                return details;
            }
        }
    }
}