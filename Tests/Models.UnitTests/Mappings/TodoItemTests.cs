using System;
using AutoFixture;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;
using NUnit.Framework;
using System.Linq;

namespace Todo.Models.UnitTests.Mappings
{
    [TestFixture]
    public class TodoItemTests : MappingSetUpFixture
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void NullObjectReturnsNull()
        {
            TodoItem item = null;
            var details = _mapper.Map<TodoItemDetails>(item);
            Assert.IsNull(details);
        }

        [Test]
        public void MapsItemToItemDetails()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                ParentItemId = Guid.NewGuid(),
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                DueDate = _fixture.Create<DateTime?>(),
                StartedOn = _fixture.Create<DateTime?>(),
                CancelledOn = _fixture.Create<DateTime?>(),
                CompletedOn = _fixture.Create<DateTime?>(),
            };
            var status = item.GetStatus();
            var details = _mapper.Map<TodoItemDetails>(item);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(details);
                Assert.IsInstanceOf<TodoItemDetails>(details);
                Assert.AreEqual(item.ItemId, details.ItemId);
                Assert.AreEqual(item.ParentItemId, details.ParentItemId);
                Assert.AreEqual(item.Name, details.Name);
                Assert.AreEqual(item.Description, details.Description);
                Assert.AreEqual(item.DueDate, details.DueDate);
                Assert.AreEqual(item.StartedOn, details.StartedOn);
                Assert.AreEqual(item.CancelledOn, details.CancelledOn);
                Assert.AreEqual(item.CompletedOn, details.CompletedOn);
                Assert.AreEqual(status, details.Status);
            });
        }

        [Test]
        public void MapsItemWithChildItemsToItemDetails()
        {
            var parentItem = GenerateItem();
            var parentItemStatus = parentItem.GetStatus();
            var childItem = GenerateItem();
            var childItemStatus = childItem.GetStatus();

            parentItem.ChildItems.Add(childItem);

            var details = _mapper.Map<TodoItemDetails>(parentItem);
            var childDetails = details.ChildItems.First();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(details);
                Assert.IsInstanceOf<TodoItemDetails>(details);
                Assert.AreEqual(parentItem.ItemId, details.ItemId);
                Assert.AreEqual(parentItem.ParentItemId, details.ParentItemId);
                Assert.AreEqual(parentItem.Name, details.Name);
                Assert.AreEqual(parentItem.Description, details.Description);
                Assert.AreEqual(parentItem.DueDate, details.DueDate);
                Assert.AreEqual(parentItem.StartedOn, details.StartedOn);
                Assert.AreEqual(parentItem.CancelledOn, details.CancelledOn);
                Assert.AreEqual(parentItem.CompletedOn, details.CompletedOn);
                Assert.AreEqual(parentItemStatus, details.Status);
                Assert.IsNotNull(childDetails);
                Assert.IsInstanceOf<TodoItemDetails>(childDetails);
                Assert.AreEqual(childItem.ItemId, childDetails.ItemId);
                Assert.AreEqual(childItem.ParentItemId, childDetails.ParentItemId);
                Assert.AreEqual(childItem.Name, childDetails.Name);
                Assert.AreEqual(childItem.Description, childDetails.Description);
                Assert.AreEqual(childItem.DueDate, childDetails.DueDate);
                Assert.AreEqual(childItem.StartedOn, childDetails.StartedOn);
                Assert.AreEqual(childItem.CancelledOn, childDetails.CancelledOn);
                Assert.AreEqual(childItem.CompletedOn, childDetails.CompletedOn);
                Assert.AreEqual(childItemStatus, childDetails.Status);
            });
        }

        private TodoItem GenerateItem()
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
    }
}