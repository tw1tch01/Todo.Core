using Todo.Services.TodoItems.Queries.GetItem;
using Todo.Services.TodoItems.Queries.Lookups.ChildItems;
using Todo.Services.TodoItems.Queries.Lookups.ParentItems;

namespace Todo.Application.Services.TodoItems.ItemQueries
{
    public interface IItemsQueryService : IGetItemService,
                                          IChildItemsLookupService,
                                          IParentItemsLookupService
    {
    }
}