using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemAlreadyStartedResultTests
    {
        [Test]
        public void IsInstanceOf_ItemInvalidResult()
        {
            var result = new ItemAlreadyStartedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemInvalidResult>(result);
        }

        [Test]
        public void Data_ContainsStartedOnKey()
        {
            var startedOn = DateTime.UtcNow;
            var startedOnKey = "StartedOn";
            var result = new ItemAlreadyStartedResult(Guid.NewGuid(), startedOn);

            Assert.Contains(startedOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtStartedOnKey_EqualsStartedOnDate()
        {
            var startedOn = DateTime.UtcNow;
            var startedOnKey = "StartedOn";
            var result = new ItemAlreadyStartedResult(Guid.NewGuid(), startedOn);

            Assert.AreEqual(startedOn, result.Data[startedOnKey]);
        }
    }
}