using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class GetItemsByParentIdTests
    {
        [Test]
        public void IsSatisfiedBy_WhenParentItemIdIsNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                ParentItemId = null
            };
            var specification = new GetItemsByParentId(Guid.NewGuid());
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenParentItemIdDoesNotMatchValuePassedIn_ReturnsFalse()
        {
            var item = new TodoItem
            {
                ParentItemId = Guid.NewGuid()
            };
            var specification = new GetItemsByParentId(Guid.NewGuid());
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenParentItemIdMatchesValuePassedIn_ReturnsTrue()
        {
            var parentId = Guid.NewGuid();
            var item = new TodoItem
            {
                ParentItemId = parentId
            };
            var specification = new GetItemsByParentId(parentId);
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }
    }
}