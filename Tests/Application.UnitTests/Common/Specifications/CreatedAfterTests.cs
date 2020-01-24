using System;
using NUnit.Framework;
using Todo.Application.Common.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.UnitTests.Common.Specifications
{
    [TestFixture]
    public class CreatedAfterTests
    {
        [Test]
        public void IsSatisfiedBy_WhenCreatedAfterIsBeforeValue_ReturnsFalse()
        {
            var createdAfter = DateTime.UtcNow;
            var item = new TodoItem
            {
                CreatedOn = createdAfter
            };
            var specification = new CreatedAfter<TodoItem>(createdAfter.AddDays(1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedBy_WhenCreatedAfterIsAfterValue_ReturnsTrue()
        {
            var createdAfter = DateTime.UtcNow;
            var item = new TodoItem
            {
                CreatedOn = createdAfter
            };
            var specification = new CreatedAfter<TodoItem>(createdAfter.AddDays(-1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisified);
        }
    }
}