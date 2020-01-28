using System;
using System.Collections.Generic;
using NUnit.Framework;
using Todo.Application.TodoItems.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class WithinItemIdsTests
    {
        [Test]
        public void IsSatisfiedBy_WhenValueIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new WithinItemIds(null));
        }

        [Test]
        public void IsSatisfiedBy_WhenValueIsEmpty_ThrowsArgumentNullException()
        {
            ICollection<Guid> collection = new List<Guid>();
            Assert.Throws<ArgumentException>(() => new WithinItemIds(collection));
        }

        [Test]
        public void IsSatisfiedBy_WhenValueDoesNotContainItemId_ReturnsFalse()
        {
            var item = new TodoItem { ItemId = Guid.NewGuid() };
            var itemIds = new List<Guid> { Guid.NewGuid() };
            var withinItemIds = new WithinItemIds(itemIds);
            var satisfied = withinItemIds.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenValueContainsItemId_ReturnsTrue()
        {
            var item = new TodoItem { ItemId = Guid.NewGuid() };
            var itemIds = new List<Guid> { item.ItemId };
            var withinItemIds = new WithinItemIds(itemIds);
            var satisfied = withinItemIds.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }
    }
}