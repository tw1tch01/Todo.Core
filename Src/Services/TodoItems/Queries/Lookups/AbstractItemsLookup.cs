using System;
using System.Collections.Generic;
using System.Linq;
using Data.Specifications;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.TodoItems.Enums;
using Todo.Services.Common.Specifications;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.TodoItems.Queries.Lookups
{
    public abstract class AbstractItemsLookup
    {
        protected LinqSpecification<TodoItem> _specification;

        public AbstractItemsLookup(LinqSpecification<TodoItem> defaultSpecifcation)
        {
            _specification = defaultSpecifcation;
        }

        #region Methods

        protected AbstractItemsLookup WithParameters(TodoItemLookupParams parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.CreatedAfter.HasValue) CreatedAfter(parameters.CreatedAfter.Value);

            if (parameters.CreatedBefore.HasValue) CreatedBefore(parameters.CreatedBefore.Value);

            if (!string.IsNullOrWhiteSpace(parameters.SearchBy)) SearchBy(parameters.SearchBy);

            if (parameters.ItemIds.Any()) WithinItemIds(parameters.ItemIds);

            if (parameters.FilterByStatus.HasValue) FilterByStatus(parameters.FilterByStatus.Value);

            if (parameters.FilterByImportance.HasValue) FilterByImportance(parameters.FilterByImportance.Value);

            if (parameters.FilterByPriority.HasValue) FilterByPriority(parameters.FilterByPriority.Value);

            if (parameters.SortBy.HasValue) SortBy(parameters.SortBy.Value);

            return this;
        }

        #endregion Methods

        #region Private Methods

        private void CreatedAfter(DateTime createdAfter)
        {
            AndSpecification(new CreatedAfter<TodoItem>(createdAfter));
        }

        private void CreatedBefore(DateTime createdBefore)
        {
            AndSpecification(new CreatedBefore<TodoItem>(createdBefore));
        }

        private void SearchBy(string searchBy)
        {
            AndSpecification(new SearchByName(searchBy));
        }

        private void WithinItemIds(ICollection<Guid> itemIds)
        {
            AndSpecification(new WithinItemIds(itemIds));
        }

        private void FilterByStatus(TodoItemStatus status)
        {
            AndSpecification(new FilterItemsByStatus(status));
        }

        private void FilterByImportance(ImportanceLevel importance)
        {
            AndSpecification(new FilterItemsByImportance(importance));
        }

        private void FilterByPriority(PriorityLevel priority)
        {
            AndSpecification(new FilterItemsByPriortyLevel(priority));
        }

        private void SortBy(SortTodoItemsBy sortBy)
        {
            switch (sortBy)
            {
                case SortTodoItemsBy.NameAsc:
                    _specification.OrderBy(item => item.Name);
                    break;

                case SortTodoItemsBy.NameDesc:
                    _specification.OrderByDescending(item => item.Name);
                    break;

                case SortTodoItemsBy.DueDateAsc:
                    _specification.OrderBy(item => item.DueDate);
                    break;

                case SortTodoItemsBy.DueDateDesc:
                    _specification.OrderByDescending(item => item.DueDate);
                    break;

                //case SortTodoItemsBy.StatusAsc:
                //    _specification.OrderBy(item => !item.CancelledOn.HasValue)
                //                  .OrderBy(item => !item.CompletedOn.HasValue)
                //                  .OrderBy(item => !item.StartedOn.HasValue)
                //                  .OrderBy(item => !(item.DueDate.HasValue && item.DueDate < DateTime.UtcNow));
                //    break;

                //case SortTodoItemsBy.StatusDesc:
                //    _specification.OrderByDescending(item => !item.CancelledOn.HasValue)
                //                  .OrderByDescending(item => !item.CompletedOn.HasValue)
                //                  .OrderByDescending(item => !item.StartedOn.HasValue)
                //                  .OrderByDescending(item => !(item.DueDate.HasValue && item.DueDate < DateTime.UtcNow));
                //    break;

                case SortTodoItemsBy.ImportanceAsc:
                    _specification.OrderBy(item => item.ImportanceLevel);
                    break;

                case SortTodoItemsBy.ImportanceDesc:
                    _specification.OrderByDescending(item => item.ImportanceLevel);
                    break;

                case SortTodoItemsBy.PriorityAsc:
                    _specification.OrderBy(item => item.PriorityLevel);
                    break;

                case SortTodoItemsBy.PriorityDesc:
                    _specification.OrderByDescending(item => item.PriorityLevel);
                    break;
            }
        }

        private void AndSpecification(LinqSpecification<TodoItem> filterSpec)
        {
            if (_specification == null) _specification = filterSpec;
            else _specification &= filterSpec;
        }

        #endregion Private Methods
    }
}