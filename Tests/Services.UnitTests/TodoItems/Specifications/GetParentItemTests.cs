using System;
using NUnit.Framework;
using Todo.Application.TodoItems.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class GetParentItemTests
    {
        [Test]
        public void IsSatisfiedBy_WhenItemIsNotParentItem_ReturnsFalse()
        {
            var item = new TodoItem { ParentItemId = Guid.NewGuid() };
            var isParentItem = new GetParentItems();
            var satisfied = isParentItem.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenItemIsParentItem_ReturnsTrue()
        {
            var item = new TodoItem { ParentItemId = null };
            var isParentItem = new GetParentItems();
            var satisfied = isParentItem.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }
    }
}