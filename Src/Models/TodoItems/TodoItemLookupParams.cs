﻿using System;
using System.Collections.Generic;
using Todo.Models.TodoItems.Enums;

namespace Todo.Models.TodoItems
{
    public class TodoItemLookupParams
    {
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string SearchBy { get; set; }
        public ICollection<Guid> ItemIds { get; set; } = new List<Guid>();
        public FilterTodoItemsBy.Status? FilterByStatus { get; set; }
        public FilterTodoItemsBy.Importance? FilterByImportance { get; set; }
        public FilterTodoItemsBy.Priority? FilterByPriority { get; set; }
        public SortTodoItemsBy? SortBy { get; set; }

        public string ToQueryString()
        {
            return $"{nameof(CreatedAfter)}={CreatedAfter}&" +
                   $"{nameof(CreatedBefore)}={CreatedBefore}&" +
                   $"{nameof(SearchBy)}={SearchBy}&" +
                   $"{nameof(ItemIds)}={string.Join(',', ItemIds ?? new List<Guid>())}&" +
                   $"{nameof(FilterByStatus)}={FilterByStatus}&" +
                   $"{nameof(FilterByImportance)}={FilterByImportance}&" +
                   $"{nameof(FilterByPriority)}={FilterByPriority}&" +
                   $"{nameof(SortBy)}={SortBy}";
        }
    }
}