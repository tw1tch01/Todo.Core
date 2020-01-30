using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class GetItemByIdTests
    {
        [Test]
        public void IsSatisfiedBy_WhenItemIdMatchesSpecId_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var item = new TodoItem { ItemId = id };
            var specification = new GetItemById(id);
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenItemIdDoesNotMatchSpecId_ReturnsFalse()
        {
            var item = new TodoItem { ItemId = Guid.NewGuid() };
            var specification = new GetItemById(Guid.NewGuid());
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }
    }
}