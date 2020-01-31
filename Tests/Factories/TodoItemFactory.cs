using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.DomainModels.Common;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.TodoItems.Enums;

namespace Todo.Factories
{
    public class TodoItemFactory
    {
        private static readonly Fixture _fixture = new Fixture();

        public static TodoItem GenerateItem(Guid? parentId = null)
        {
            return new TodoItem
            {
                CreatedBy = _fixture.Create<string>(),
                CreatedOn = _fixture.Create<DateTime>(),
                CreatedProcess = _fixture.Create<string>(),
                ModifiedBy = _fixture.Create<string>(),
                ModifiedOn = _fixture.Create<DateTime?>(),
                ModifiedProcess = _fixture.Create<string>(),
                ItemId = Guid.NewGuid(),
                ParentItemId = parentId,
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                DueDate = _fixture.Create<DateTime?>(),
                StartedOn = _fixture.Create<DateTime?>(),
                CancelledOn = _fixture.Create<DateTime?>(),
                CompletedOn = _fixture.Create<DateTime?>(),
                ImportanceLevel = _fixture.Create<ImportanceLevel>(),
                PriorityLevel = _fixture.Create<PriorityLevel>()
            };
        }

        public static TodoItem GenerateItemWithChildren(int count)
        {
            var parent = GenerateItem();
            for (int i = 0; i < count; i++) parent.ChildItems.Add(GenerateItem(parent.ItemId));
            return parent;
        }

        public static TodoItemLookup GenerateItemLookup()
        {
            return new TodoItemLookup
            {
                ItemId = Guid.NewGuid(),
                Name = _fixture.Create<string>(),
                DueDate = _fixture.Create<DateTime?>(),
                Importance = _fixture.Create<ImportanceLevel>(),
                Priority = _fixture.Create<PriorityLevel>(),
                Status = _fixture.Create<TodoItemStatus>()
            };
        }

        public static ParentTodoItemLookup GenerateParentItemLookup()
        {
            var parentItem = (ParentTodoItemLookup)GenerateItemLookup();
            parentItem.ChildItems = _fixture.Create<int>();
            parentItem.Notes = _fixture.Create<int>();
            return parentItem;
        }

        public static TodoItemLookupParams GenerateItemLookupParams()
        {
            return new TodoItemLookupParams
            {
                CreatedAfter = _fixture.Create<DateTime?>(),
                CreatedBefore = _fixture.Create<DateTime?>(),
                FilterByStatus = _fixture.Create<TodoItemStatus>(),
                FilterByImportance = _fixture.Create<ImportanceLevel>(),
                FilterByPriority = _fixture.Create<PriorityLevel>(),
                SearchBy = _fixture.Create<string>(),
                SortBy = _fixture.Create<SortTodoItemsBy>(),
                ItemIds = _fixture.Create<ICollection<Guid>>()
            };
        }

        public static CreateItemDto GenerateCreateDto()
        {
            return new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                DueDate = _fixture.Create<DateTime?>(),
                Importance = _fixture.Create<ImportanceLevel>(),
                Priority = _fixture.Create<PriorityLevel>()
            };
        }

        #region Mapped

        public static TodoItemDetails MappedItemDetails(TodoItem item)
        {
            return new TodoItemDetails
            {
                Created = new CreatedAuditInfo
                {
                    By = item.CreatedBy,
                    On = item.CreatedOn,
                    Process = item.CreatedProcess
                },
                Modified = new ModifiedAuditInfo
                {
                    By = item.ModifiedBy,
                    On = item.ModifiedOn,
                    Process = item.ModifiedProcess
                },
                ItemId = item.ItemId,
                ParentItemId = item.ParentItemId,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                StartedOn = item.StartedOn,
                CancelledOn = item.CancelledOn,
                CompletedOn = item.CompletedOn,
                Status = item.GetStatus(),
                ChildItems = item.ChildItems.Select(MappedItemLookup).ToList()
            };
        }

        public static TodoItemLookup MappedItemLookup(TodoItem item)
        {
            return new TodoItemLookup
            {
                ItemId = item.ItemId,
                Name = item.Name,
                DueDate = item.DueDate,
                Importance = item.ImportanceLevel,
                Priority = item.PriorityLevel,
                Status = item.GetStatus()
            };
        }

        public static ParentTodoItemLookup MappedParentItemLookup(TodoItem item)
        {
            return new ParentTodoItemLookup
            {
                ItemId = item.ItemId,
                Name = item.Name,
                DueDate = item.DueDate,
                Importance = item.ImportanceLevel,
                Priority = item.PriorityLevel,
                Status = item.GetStatus(),
                ChildItems = item.ChildItems.Count,
                Notes = item.Notes.Count
            };
        }

        public static TodoItem MappedItem(CreateItemDto dto)
        {
            return new TodoItem
            {
                Name = dto.Name,
                Description = dto.Description,
                DueDate = dto.DueDate,
                ImportanceLevel = dto.Importance,
                PriorityLevel = dto.Priority
            };
        }

        #endregion Mapped
    }
}