using System;
using NUnit.Framework;
using Todo.Application.TodoItems.Queries.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.UnitTests.TodoItems.Queries.Specifications
{
    [TestFixture]
    public class GetItemsByParentIdTests
    {
        #region Specification

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

        #endregion Specification
    }
}