using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.Common.Specifications;

namespace Todo.Services.UnitTests.Common.Specifications
{
    [TestFixture]
    public class CreatedBeforeTests
    {
        [Test]
        public void IsSatisfiedBy_WhenCreatedBeforeIsAfterValue_ReturnsFalse()
        {
            var createdBefore = DateTime.UtcNow;
            var item = new TodoItem
            {
                CreatedOn = createdBefore
            };
            var specification = new CreatedBefore<TodoItem>(createdBefore.AddDays(-1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedBy_WhenCreatedBeforeIsBeforeValue_ReturnsTrue()
        {
            var createdBefore = DateTime.UtcNow;
            var item = new TodoItem
            {
                CreatedOn = createdBefore
            };
            var specification = new CreatedBefore<TodoItem>(createdBefore.AddDays(1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisified);
        }
    }
}