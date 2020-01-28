using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Data.Specifications;
using Todo.Application.Common.Specifications;
using Todo.Application.TodoItems.Specifications;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;
using Todo.Models.TodoItems.Enums;

[assembly: InternalsVisibleTo("Todo.Application.UnitTests")]

namespace Todo.Application.TodoItems.Queries.Lookup
{
    internal static class AbstractItemsLookupExtensions
    {
        internal static AbstractItemsLookup WithFilters(this AbstractItemsLookup request, TodoItemLookupParams parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.CreatedAfter.HasValue) request.CreatedAfter(parameters.CreatedAfter.Value);

            if (parameters.CreatedBefore.HasValue) request.CreatedBefore(parameters.CreatedBefore.Value);

            if (!string.IsNullOrWhiteSpace(parameters.SearchBy)) request.SearchBy(parameters.SearchBy);

            if (parameters.ItemIds.Any()) request.WithinItemIds(parameters.ItemIds);

            if (parameters.FilterByStatus.HasValue) request.FilterByStatus(parameters.FilterByStatus.Value);

            if (parameters.FilterByImportance.HasValue) request.FilterByImportance(parameters.FilterByImportance.Value);

            if (parameters.FilterByPriority.HasValue) request.FilterByPriority(parameters.FilterByPriority.Value);

            if (parameters.SortBy.HasValue) request.SortBy(parameters.SortBy.Value);

            return request;
        }
    }

    internal abstract class AbstractItemsLookup
    {
        public AbstractItemsLookup(LinqSpecification<TodoItem> defaultSpecifcation)
        {
            Specification = defaultSpecifcation;
        }

        protected LinqSpecification<TodoItem> Specification { get; private set; }

        #region Methods

        public void CreatedAfter(DateTime createdAfter)
        {
            AndSpecification(new CreatedAfter<TodoItem>(createdAfter));
        }

        public void CreatedBefore(DateTime createdBefore)
        {
            AndSpecification(new CreatedBefore<TodoItem>(createdBefore));
        }

        public void SearchBy(string searchBy)
        {
            AndSpecification(new SearchByName(searchBy));
        }

        public void WithinItemIds(ICollection<Guid> itemIds)
        {
            AndSpecification(new WithinItemIds(itemIds));
        }

        public void FilterByStatus(FilterTodoItemsBy.Status status)
        {
            AndSpecification(new FilterItemsBy.Status(status));
        }

        public void FilterByImportance(FilterTodoItemsBy.Importance importance)
        {
            AndSpecification(new FilterItemsBy.ImportanceLevel(importance));
        }

        public void FilterByPriority(FilterTodoItemsBy.Priority priority)
        {
            AndSpecification(new FilterItemsBy.PriortyLevel(priority));
        }

        public void SortBy(SortTodoItemsBy sortBy)
        {
            switch (sortBy)
            {
                case SortTodoItemsBy.NameAsc:
                    Specification.OrderBy(item => item.Name);
                    break;

                case SortTodoItemsBy.NameDesc:
                    Specification.OrderByDescending(item => item.Name);
                    break;

                case SortTodoItemsBy.DueDateAsc:
                    Specification.OrderBy(item => item.DueDate);
                    break;

                case SortTodoItemsBy.DueDateDesc:
                    Specification.OrderByDescending(item => item.DueDate);
                    break;

                case SortTodoItemsBy.StatusAsc:
                    Specification.OrderBy(item => item.GetStatus());
                    break;

                case SortTodoItemsBy.StatusDesc:
                    Specification.OrderByDescending(item => item.GetStatus());
                    break;

                case SortTodoItemsBy.ImportanceAsc:
                    Specification.OrderBy(item => item.ImportanceLevel);
                    break;

                case SortTodoItemsBy.ImportanceDesc:
                    Specification.OrderByDescending(item => item.ImportanceLevel);
                    break;

                case SortTodoItemsBy.PriorityAsc:
                    Specification.OrderBy(item => item.PriorityLevel);
                    break;

                case SortTodoItemsBy.PriorityDesc:
                    Specification.OrderByDescending(item => item.PriorityLevel);
                    break;
            }
        }

        #endregion Methods

        #region Private Methods

        protected void AndSpecification(LinqSpecification<TodoItem> filterSpec)
        {
            if (Specification == null) Specification = filterSpec;
            else Specification &= filterSpec;
        }

        #endregion Private Methods
    }
}