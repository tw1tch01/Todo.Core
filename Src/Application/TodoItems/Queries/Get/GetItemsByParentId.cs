using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Data.Specifications;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

[assembly: InternalsVisibleTo("Todo.Application.UnitTests")]

namespace Todo.Application.TodoItems.Queries.Get
{
    internal class GetItemsByParentId : LinqSpecification<TodoItem>, IRequest<ICollection<TodoItemDetails>>
    {
        private readonly Guid _parentId;

        public GetItemsByParentId(Guid parentId)
        {
            _parentId = parentId;
        }

        public override Expression<Func<TodoItem, bool>> AsExpression()
        {
            return item => item.ParentItemId == _parentId;
        }

        internal class Handler : IRequestHandler<GetItemsByParentId, ICollection<TodoItemDetails>>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public Handler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<ICollection<TodoItemDetails>> Handle(GetItemsByParentId request, CancellationToken cancellationToken)
            {
                var items = await _repository.ListAsync(request);

                var details = _mapper.Map<ICollection<TodoItemDetails>>(items);

                return details;
            }
        }
    }
}