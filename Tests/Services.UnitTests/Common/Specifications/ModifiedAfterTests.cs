using System;
using NUnit.Framework;
using Todo.Services.Common.Specifications;
using Todo.Domain.Entities;

namespace Todo.Services.UnitTests.Common.Specifications
{
    [TestFixture]
    public class ModifiedAfterTests
    {
        [Test]
        public void IsSatisfiedBy_WhenModifiedAfterIsNull_ReturnsFalse()
        {
            var modifiedAfter = DateTime.UtcNow;
            var item = new TodoItem
            {
                ModifiedOn = null
            };
            var specification = new ModifiedAfter<TodoItem>(modifiedAfter.AddDays(1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedBy_WhenModifiedAfterIsBeforeValue_ReturnsFalse()
        {
            var modifiedAfter = DateTime.UtcNow;
            var item = new TodoItem
            {
                ModifiedOn = modifiedAfter
            };
            var specification = new ModifiedAfter<TodoItem>(modifiedAfter.AddDays(1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisified);
        }

        [Test]
        public void IsSatisfiedBy_WhenModifiedAfterIsAfterValue_ReturnsTrue()
        {
            var modifiedAfter = DateTime.UtcNow;
            var item = new TodoItem
            {
                ModifiedOn = modifiedAfter
            };
            var specification = new ModifiedAfter<TodoItem>(modifiedAfter.AddDays(-1));
            var satisified = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisified);
        }
    }
}