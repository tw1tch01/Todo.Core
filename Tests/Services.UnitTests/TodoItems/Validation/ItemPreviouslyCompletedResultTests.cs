using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemPreviouslyCompletedResultTests
    {
        [Test]
        public void IsInstanceOf_ItemInvalidResult()
        {
            var result = new ItemPreviouslyCompletedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemInvalidResult>(result);
        }

        [Test]
        public void Data_ContainsCompletedOnKey()
        {
            var completedOn = DateTime.UtcNow;
            var completedOnKey = "CompletedOn";
            var result = new ItemPreviouslyCompletedResult(Guid.NewGuid(), completedOn);

            Assert.Contains(completedOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtCompletedOnKey_EqualsCompletedOnDate()
        {
            var completedOn = DateTime.UtcNow;
            var completedOnKey = "CompletedOn";
            var result = new ItemPreviouslyCompletedResult(Guid.NewGuid(), completedOn);

            Assert.AreEqual(completedOn, result.Data[completedOnKey]);
        }
    }
}