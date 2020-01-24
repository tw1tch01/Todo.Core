using System;
using System.Collections.Generic;
using Todo.Models.TodoItems.Enums;

namespace Todo.Models.TodoItems
{
    public class TodoItemLookupParams
    {
        public TodoItemLookupParams()
        {
            ItemIds = new List<Guid>();
        }

        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string SearchBy { get; set; }
        public ICollection<Guid> ItemIds { get; set; }
        public FilterTodoItemsBy.Status? FilterByStatus { get; set; }
        public FilterTodoItemsBy.Importance? FilterByImportance { get; set; }
        public FilterTodoItemsBy.Priority? FilterByPriority { get; set; }
        public SortTodoItemsBy? SortBy { get; set; }
    }
}