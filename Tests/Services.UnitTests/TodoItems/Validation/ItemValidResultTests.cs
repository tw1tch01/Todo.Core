using System;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemValidResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidationResult()
        {
            var result = new TestResult();

            Assert.IsInstanceOf<ItemValidationResult>(result);
        }

        [Test]
        public void IsValid_ReturnsTrue()
        {
            var result = new TestResult();

            Assert.IsTrue(result.IsValid);
        }

        private class TestResult : ItemValidResult
        {
            public TestResult()
                : base(Guid.NewGuid(), string.Empty)
            {
            }
        }
    }
}