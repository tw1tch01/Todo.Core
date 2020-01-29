using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.DomainModels.TodoItems;
using Todo.Services.TodoItems.Queries.GetItem;
using Todo.Services.TodoItems.Queries.Lookups.ChildItems;
using Todo.Services.TodoItems.Queries.Lookups.ParentItems;

namespace Todo.Application.Services.TodoItems.ItemQueries
{
    public class ItemsQueryService : IItemsQueryService
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

        public async Task<TodoItemDetails> GetItem(Guid itemId)
        {
            return await _getItemService.GetItem(itemId);
        }

        public async Task<ICollection<TodoItemLookup>> LookupChildItems(Guid parentId)
        {
            return await _childItemsLookupService.LookupChildItems(parentId);
        }

        public async Task<ICollection<ParentTodoItemLookup>> LookupParentItems(TodoItemLookupParams parameters)
        {
            return await _parentItemsLookupService.LookupParentItems(parameters);
        }

        public async Task<PagedCollection<ParentTodoItemLookup>> PagedLookupParentItems(int page, int pageSize, TodoItemLookupParams parameters)
        {
            return await _parentItemsLookupService.PagedLookupParentItems(page, pageSize, parameters);
        }
    }
}