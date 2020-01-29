using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Queries.Lookups.ChildItems
{
    internal class ChildItemsLookupService : IChildItemsLookupService
    {
        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;

        public ChildItemsLookupService(IContextRepository<ITodoContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<TodoItemLookup>> LookupChildItems(Guid parentItemId)
        {
            var items = await _repository.ListAsync(new GetItemsByParentId(parentItemId)
                .OrderBy(i => i.Rank)
                .Include(i => i.Notes));

            var details = _mapper.Map<ICollection<TodoItemLookup>>(items);

            return details;
        }
    }
}