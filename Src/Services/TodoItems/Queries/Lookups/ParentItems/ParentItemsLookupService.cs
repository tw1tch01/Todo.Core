using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Common;
using Data.Repositories;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Queries.Lookups.ParentItems
{
    public class ParentItemsLookupService : AbstractItemsLookup, IParentItemsLookupService
    {
        private const int _defaultPageSize = 10;
        private const int _maximumPageSize = 25;

        private readonly IContextRepository<ITodoContext> _repository;
        private readonly IMapper _mapper;

        public ParentItemsLookupService(IContextRepository<ITodoContext> repository, IMapper mapper)
            : base(new GetParentItems().Include(a => a.ChildItems).Include(a => a.Notes))
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<ICollection<ParentTodoItemLookup>> LookupParentItems(TodoItemLookupParams parameters)
        {
            WithParameters(parameters);

            var parentItems = await _repository.ListAsync(_specification);

            var details = _mapper.Map<ICollection<ParentTodoItemLookup>>(parentItems);

            return details;
        }

        public virtual async Task<PagedCollection<ParentTodoItemLookup>> PagedLookupParentItems(int page, int pageSize, TodoItemLookupParams parameters)
        {
            WithParameters(parameters);

            (page, pageSize) = ValidatePaging(page, pageSize);

            var pagedCollection = await _repository.PagedListAsync(page, pageSize, _specification, item => item.ItemId);

            var details = _mapper.Map<ICollection<ParentTodoItemLookup>>(pagedCollection.Items);

            return new PagedCollection<ParentTodoItemLookup>
            (
                pagedCollection.Page,
                pagedCollection.PageSize,
                pagedCollection.TotalRecords,
                details
            );
        }

        internal static (int page, int pageSize) ValidatePaging(int page, int pageSize)
        {
            if (page < 0) page = 0;

            if (pageSize <= 0) pageSize = _defaultPageSize;
            else if (pageSize > _maximumPageSize) pageSize = _maximumPageSize;

            return (page, pageSize);
        }
    }
}