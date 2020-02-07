using System;
using Todo.DomainModels.TodoItems;

namespace Todo.Services.Cache
{
    public static class CacheKeys
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

            public static string GetItem(Guid itemId) => $"{Pattern}?itemId={itemId}";

            public static class Lookups
            {
                public static string ChildItems(Guid parentId) => $"{Pattern}?parentId={parentId}";

                public static string PagedParentItems(int page, int pageSize, TodoItemLookupParams parameters) => $"{Pattern}?Page={page}&PageSize={pageSize}&{parameters.ToQueryString()}";

                public static string ParentItems(TodoItemLookupParams parameters) => $"{Pattern}?{parameters.ToQueryString()}";
            }
        }
    }
}