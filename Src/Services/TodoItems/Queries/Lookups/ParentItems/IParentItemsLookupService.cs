using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Todo.DomainModels.TodoItems;

namespace Todo.Services.TodoItems.Queries.Lookups.ParentItems
{
    public interface IParentItemsLookupService
    {
        Task<ICollection<ParentTodoItemLookup>> LookupParentItems(TodoItemLookupParams parameters);

        Task<PagedCollection<ParentTodoItemLookup>> PagedLookupParentItems(int page, int pageSize, TodoItemLookupParams parameters);
    }
}