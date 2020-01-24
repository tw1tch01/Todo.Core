using System;
using System.Linq;
using AutoFixture;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

namespace Todo.Application.IntegrationTests.TestingFactory
{
    internal class TodoItemFactory
    {
        private static readonly Fixture _fixture = new Fixture();

        internal static TodoItem GenerateItem(Guid? parentId = null)
        {
            return new TodoItem
            {
                CreatedBy = MemoryContext.CreatedBy,
                CreatedOn = _fixture.Create<DateTime>(),
                CreatedProcess = MemoryContext.CreatedProcess,
                ModifiedBy = MemoryContext.ModifiedBy,
                ModifiedOn = _fixture.Create<DateTime>(),
                ModifiedProcess = MemoryContext.ModifiedProcess,
                ItemId = Guid.NewGuid(),
                ParentItemId = parentId,
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                DueDate = _fixture.Create<DateTime?>(),
                StartedOn = _fixture.Create<DateTime?>(),
                CancelledOn = _fixture.Create<DateTime?>(),
                CompletedOn = _fixture.Create<DateTime?>()
            };
        }

        internal static TodoItem GenerateItemWithChildren(int count)
        {
            var parent = GenerateItem();

            for (int i = 0; i < count; i++)
            {
                parent.ChildItems.Add(GenerateItem(parent.ItemId));
            }

            return parent;
        }

        internal static TodoItemDetails MappedItem(TodoItem item)
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
                ChildItems = item.ChildItems.Select(MappedItem).ToList()
            };
        }
    }
}