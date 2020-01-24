using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Specifications;
using Todo.Models.TodoItems;

[assembly: InternalsVisibleTo("Todo.Application.IntegrationTests")]
[assembly: InternalsVisibleTo("Todo.Application.UnitTests")]

namespace Todo.Application.TodoItems.Queries.Lookup
{
    internal class ParentItemsLookup : AbstractItemsLookup, IRequest<ICollection<ParentTodoItemLookup>>
    {
        public ParentItemsLookup()
            : base(new GetParentItems())
        {
        }

        internal class Handler : IRequestHandler<ParentItemsLookup, ICollection<ParentTodoItemLookup>>
        {
            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public Handler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<ICollection<ParentTodoItemLookup>> Handle(ParentItemsLookup request, CancellationToken cancellationToken)
            {
                var parentItems = await _repository.ListAsync(request.Specification);

                var details = _mapper.Map<ICollection<ParentTodoItemLookup>>(parentItems);

                return details;
            }
        }
    }
}