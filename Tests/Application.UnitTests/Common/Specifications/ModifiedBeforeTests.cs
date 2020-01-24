using System;
using NUnit.Framework;
using Todo.Application.Common.Specifications;
using Todo.Domain.Entities;

namespace Todo.Application.UnitTests.Common.Specifications
{
    [TestFixture]
    public class ModifiedBeforeTests
    {
        [Test]
        public void IsSatisfiedBy_WhenModifiedBeforeIsNull_ReturnsFalse()
        {
            var modifiedBefore = DateTime.UtcNow;
            var item = new TodoItem
            {
                ModifiedOn = null
            };
            var specification = new ModifiedBefore<TodoItem>(modifiedBefore);
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedBy_WhenModifiedBeforeIsAfterValue_ReturnsFalse()
        {
            var modifiedBefore = DateTime.UtcNow;
            var item = new TodoItem
            {
                ModifiedOn = modifiedBefore
            };
            var specification = new ModifiedBefore<TodoItem>(modifiedBefore.AddDays(-1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedBy_WhenModifiedBeforeIsBeforeValue_ReturnsTrue()
        {
            var modifiedBefore = DateTime.UtcNow;
            var item = new TodoItem
            {
                ModifiedOn = modifiedBefore
            };
            var specification = new ModifiedBefore<TodoItem>(modifiedBefore.AddDays(1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisified);
        }
    }
}