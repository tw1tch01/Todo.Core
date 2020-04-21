using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemStartedResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidResult()
        {
            var result = new ItemStartedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemValidResult>(result);
        }

        [Test]
        public void Data_ContainsStartedOnKey()
        {
            var startedOnKey = "StartedOn";
            var result = new ItemStartedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.Contains(startedOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtStartedOnKey_EqualsStartedOnDate()
        {
            var startedOn = DateTime.UtcNow;
            var startedOnKey = "StartedOn";
            var result = new ItemStartedResult(Guid.NewGuid(), startedOn);

            Assert.AreEqual(startedOn, result.Data[startedOnKey]);
        }
    }
}