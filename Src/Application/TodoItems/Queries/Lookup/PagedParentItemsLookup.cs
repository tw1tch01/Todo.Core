using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Common;
using Data.Repositories;
using MediatR;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Specifications;
using Todo.Models.TodoItems;

[assembly: InternalsVisibleTo("Todo.Application.IntegrationTests")]
[assembly: InternalsVisibleTo("Todo.Application.UnitTests")]

namespace Todo.Application.TodoItems.Queries.Lookup
{
    internal class PagedParentItemsLookup : AbstractItemsLookup, IRequest<PagedCollection<ParentTodoItemLookup>>
    {
        public PagedParentItemsLookup(int page, int pageSize)
            : base(new GetParentItems())
        {
            Page = page;
            PageSize = pageSize;
        }

        protected int Page { get; }
        protected int PageSize { get; }

        internal class RequestHandler : IRequestHandler<PagedParentItemsLookup, PagedCollection<ParentTodoItemLookup>>
        {
            private const int _defaultPageSize = 10;
            private const int _maximumPageSize = 25;

            private readonly IContextRepository<ITodoContext> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IContextRepository<ITodoContext> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PagedCollection<ParentTodoItemLookup>> Handle(PagedParentItemsLookup request, CancellationToken cancellationToken)
            {
                (int page, int pageSize) = ValidatePaging(request.Page, request.PageSize);

                request.Specification.Include(a => a.ChildItems).Include(a => a.Notes);

                var pagedCollection = await _repository.PagedListAsync(page, pageSize, request.Specification, item => item.ItemId);

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
}