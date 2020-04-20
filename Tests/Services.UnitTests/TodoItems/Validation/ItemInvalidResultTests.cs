using System;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemInvalidResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidationResult()
        {
            var result = new TestResult();

            Assert.IsInstanceOf<ItemValidationResult>(result);
        }

        [Test]
        public void IsValid_ReturnsFalse()
        {
            var result = new TestResult();

            Assert.IsFalse(result.IsValid);
        }

        private class TestResult : ItemInvalidResult
        {
            public TestResult()
                : base(Guid.NewGuid(), string.Empty)
            {
            }
        }
    }
}