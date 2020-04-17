using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.DomainModels.TodoItems;
using Todo.Services.TodoItems.Queries.GetItem;
using Todo.Services.TodoItems.Queries.Lookups.ChildItems;
using Todo.Services.TodoItems.Queries.Lookups.ParentItems;

namespace Todo.Application.Services.TodoItems
{
    public class ItemsQueryService : IGetItemService, IChildItemsLookupService, IParentItemsLookupService
    {
        private readonly IGetItemService _getItemService;
        private readonly IChildItemsLookupService _childItemsLookupService;
        private readonly IParentItemsLookupService _parentItemsLookupService;

        public ItemsQueryService
        (
            IGetItemService getItemService,
            IChildItemsLookupService childItemsLookupService,
            IParentItemsLookupService parentItemsLookupService
        )
        {
            _getItemService = getItemService;
            _childItemsLookupService = childItemsLookupService;
            _parentItemsLookupService = parentItemsLookupService;
        }

        public Task<TodoItemDetails> GetItem(Guid itemId) => _getItemService.GetItem(itemId);

        public Task<ICollection<TodoItemLookup>> LookupChildItems(Guid parentId) => _childItemsLookupService.LookupChildItems(parentId);

        public Task<ICollection<ParentTodoItemLookup>> LookupParentItems(TodoItemLookupParams parameters) => _parentItemsLookupService.LookupParentItems(parameters);

        public Task<PagedCollection<ParentTodoItemLookup>> PagedLookupParentItems(int page, int pageSize, TodoItemLookupParams parameters) => _parentItemsLookupService.PagedLookupParentItems(page, pageSize, parameters);
    }
}