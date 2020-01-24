using System;
using System.Linq;
using AutoFixture;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

namespace Todo.Application.UnitTests.TestingFactories
{
    internal class TodoItemFactory
    {
        private static Fixture _fixture = new Fixture();

        public static TodoItem GenerateItem()
        {
            return new TodoItem
            {
                ItemId = Guid.NewGuid(),
                ParentItemId = Guid.NewGuid(),
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                DueDate = _fixture.Create<DateTime?>(),
                StartedOn = _fixture.Create<DateTime?>(),
                CancelledOn = _fixture.Create<DateTime?>(),
                CompletedOn = _fixture.Create<DateTime?>()
            };
        }

        public static TodoItemDetails MappedItemDetails(TodoItem item)
        {
            return new TodoItemDetails
            {
                ItemId = item.ItemId,
                ParentItemId = item.ParentItemId,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                StartedOn = item.StartedOn,
                CancelledOn = item.CancelledOn,
                CompletedOn = item.CompletedOn,
                Status = item.GetStatus(),
                ChildItems = item.ChildItems.Select(MappedItemDetails).ToList()
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
    }
}