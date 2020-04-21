using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.Common.Validation;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemValidationResultTests
    {
        [Test]
        public void IsInstanceOf_ValidationResult()
        {
            var result = new TestResult();

            Assert.IsInstanceOf<ValidationResult>(result);
        }

        [Test]
        public void Data_ContainsItemIdKey()
        {
            var itemIdKey = "ItemId";
            var result = new TestResult();

            Assert.Contains(itemIdKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtItemIdKey_EqualsItemId()
        {
            var itemId = Guid.NewGuid();
            var itemIdKey = "ItemId";
            var result = new TestResult(itemId);

            Assert.AreEqual(itemId, result.Data[itemIdKey]);
        }

        private class TestResult : ItemValidationResult
        {
            public TestResult(Guid itemId = default, bool isValid = default, string message = default)
                : base(itemId, isValid, message)
            {
            }
        }
    }
}