using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.DomainModels.TodoItems;

namespace Todo.Services.TodoItems.Queries.Lookups.ChildItems
{
    public interface IChildItemsLookupService
    {
        Task<ICollection<TodoItemLookup>> LookupChildItems(Guid parentItemId);
    }
}