using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemResetResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidResult()
        {
            var result = new ItemResetResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemValidResult>(result);
        }

        [Test]
        public void Data_ContainsResetOnKey()
        {
            var resetOnKey = "ResetOn";
            var result = new ItemResetResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.Contains(resetOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtResetOnKey_EqualsResetOnDate()
        {
            var resetOn = DateTime.UtcNow;
            var resetOnKey = "ResetOn";
            var result = new ItemResetResult(Guid.NewGuid(), resetOn);

            Assert.AreEqual(resetOn, result.Data[resetOnKey]);
        }
    }
}