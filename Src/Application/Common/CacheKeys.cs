using System;
using Todo.Models.TodoItems;

namespace Todo.Application.Common
{
    internal static class CacheKeys
    {
        public static class Times
        {
            public const int ShortTime = 5;

            public const int Hour = 60;

            public const int Default = 120;

            public const int LongTime = 720;
        }

        public static class Items
        {
            private const string Pattern = "/items";

            public static string Item(Guid itemId) => $"{Pattern}?itemId={itemId}";

            public static string ListItems(TodoItemLookupParams parameters) => $"{Pattern}?{parameters.ToQueryString()}";

            public static string PagedItems(int page, int pageSize, TodoItemLookupParams parameters) => $"{Pattern}?Page={page}&PageSize={pageSize}&{parameters.ToQueryString()}";

            public static string ChildItems(Guid parentId) => $"{Pattern}?parentId={parentId}";
        }
    }
}