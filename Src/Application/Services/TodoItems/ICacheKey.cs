using System;
using Todo.Models.TodoItems;

namespace Todo.Application.Services.TodoItems
{
    public static class CacheKey
    {
        public static class Time
        {
            public const int ShortTime = 5;

            public const int Hour = 60;

            public const int Default = 120;

            public const int LongTime = 720;
        }

        public static class Items
        {
            public const string Pattern = "/items";

            public static string Item(Guid itemId) => $"{Pattern}?itemId={itemId}";

            public static string ListItems(TodoItemLookupParams parameters) => $"{Pattern}?{parameters.ToQueryString()}";

            public static string PagedItems(int page, int pageSize, TodoItemLookupParams parameters) => $"{Pattern}?Page={page}&PageSize={pageSize}&{parameters.ToQueryString()}";

            public static string ChildItems(Guid parentId) => $"{Pattern}?parentId={parentId}";
        }
    }
}