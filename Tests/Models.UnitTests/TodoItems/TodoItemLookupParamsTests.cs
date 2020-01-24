using System;
using System.Collections.Generic;
using AutoFixture;
using NUnit.Framework;
using Todo.Models.TodoItems;
using Todo.Models.TodoItems.Enums;

namespace Todo.Models.UnitTests.TodoItems
{
    [TestFixture]
    public class TodoItemLookupParamsTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void ToQueryString_WhenNoFiltersApplied_ReturnsNullParameters()
        {
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={null}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={null}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={null}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = null,
                CreatedBefore = null,
                SearchBy = null,
                ItemIds = null,
                FilterByStatus = null,
                FilterByImportance = null,
                FilterByPriority = null,
                SortBy = null
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }

        [Test]
        public void ToQueryString_WithCreatedAfter_ReturnsQueryStringWithSetCreatedAfterValue()
        {
            var createdAfter = DateTime.UtcNow;
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={createdAfter}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={null}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={null}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = createdAfter,
                CreatedBefore = null,
                SearchBy = null,
                ItemIds = null,
                FilterByStatus = null,
                FilterByImportance = null,
                FilterByPriority = null,
                SortBy = null
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }

        [Test]
        public void ToQueryString_WithCreatedBefore_ReturnsQueryStringWithSetCreatedBeforeValue()
        {
            var createdBefore = DateTime.UtcNow;
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={null}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={createdBefore}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={null}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={null}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = null,
                CreatedBefore = createdBefore,
                SearchBy = null,
                ItemIds = null,
                FilterByStatus = null,
                FilterByImportance = null,
                FilterByPriority = null,
                SortBy = null
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }

        [Test]
        public void ToQueryString_WithSearchBy_ReturnsQueryStringWithSetSearchByValue()
        {
            var searchBy = _fixture.Create<string>();
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={null}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={searchBy}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={null}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = null,
                CreatedBefore = null,
                SearchBy = searchBy,
                ItemIds = null,
                FilterByStatus = null,
                FilterByImportance = null,
                FilterByPriority = null,
                SortBy = null
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }

        [Test]
        public void ToQueryString_WithItemIds_ReturnsQueryStringWithSetItemIdsValue()
        {
            var itemIds = new List<Guid> { Guid.NewGuid() };
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={null}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={null}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={string.Join(',', itemIds)}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={null}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = null,
                CreatedBefore = null,
                SearchBy = null,
                ItemIds = itemIds,
                FilterByStatus = null,
                FilterByImportance = null,
                FilterByPriority = null,
                SortBy = null
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }

        [Test]
        public void ToQueryString_WithFilterByStatus_ReturnsQueryStringWithSetFilterByStatusValue()
        {
            var status = FilterTodoItemsBy.Status.Cancelled;
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={null}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={null}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={status}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={null}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = null,
                CreatedBefore = null,
                SearchBy = null,
                ItemIds = null,
                FilterByStatus = status,
                FilterByImportance = null,
                FilterByPriority = null,
                SortBy = null
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }

        [Test]
        public void ToQueryString_WithFilterByImportance_ReturnsQueryStringWithSetFilterByImportanceValue()
        {
            var importance = FilterTodoItemsBy.Importance.Critical;
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={null}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={null}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={importance}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={null}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = null,
                CreatedBefore = null,
                SearchBy = null,
                ItemIds = null,
                FilterByStatus = null,
                FilterByImportance= importance,
                FilterByPriority = null,
                SortBy = null
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }

        [Test]
        public void ToQueryString_WithFilterByPriority_ReturnsQueryStringWithSetFilterByPriorityValue()
        {
            var priority = FilterTodoItemsBy.Priority.Medium;
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={null}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={null}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={priority}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={null}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = null,
                CreatedBefore = null,
                SearchBy = null,
                ItemIds = null,
                FilterByStatus = null,
                FilterByImportance= null,
                FilterByPriority = priority,
                SortBy = null
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }

        [Test]
        public void ToQueryString_WithFilterBySortBy_ReturnsQueryStringWithSetSortByValue()
        {
            var sortBy = SortTodoItemsBy.NameAsc;
            var expected = $"{nameof(TodoItemLookupParams.CreatedAfter)}={null}&" +
                           $"{nameof(TodoItemLookupParams.CreatedBefore)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SearchBy)}={null}&" +
                           $"{nameof(TodoItemLookupParams.ItemIds)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByStatus)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByImportance)}={null}&" +
                           $"{nameof(TodoItemLookupParams.FilterByPriority)}={null}&" +
                           $"{nameof(TodoItemLookupParams.SortBy)}={sortBy}";
            var filters = new TodoItemLookupParams
            {
                CreatedAfter = null,
                CreatedBefore = null,
                SearchBy = null,
                ItemIds = null,
                FilterByStatus = null,
                FilterByImportance= null,
                FilterByPriority = null,
                SortBy = sortBy
            };
            var queryString = filters.ToQueryString();
            Assert.AreEqual(expected, queryString);
        }
    }
}