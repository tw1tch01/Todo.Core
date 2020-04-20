using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemCompletedResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidResult()
        {
            var result = new ItemCompletedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemValidResult>(result);
        }

        [Test]
        public void Data_ContainsCompletedOnKey()
        {
            var completedOnKey = "CompletedOn";
            var result = new ItemCompletedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.Contains(completedOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtCompletedOnKey_EqualsCompletedOnDate()
        {
            var completedOn = DateTime.UtcNow;
            var completedOnKey = "CompletedOn";
            var result = new ItemCompletedResult(Guid.NewGuid(), completedOn);

            Assert.AreEqual(completedOn, result.Data[completedOnKey]);
        }
    }
}