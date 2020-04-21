using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.Common.Specifications;

namespace Todo.Services.UnitTests.Common.Specifications
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