using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemCancelledResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidResult()
        {
            var result = new ItemCancelledResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemValidResult>(result);
        }

        [Test]
        public void Data_ContainsCancelledOnKey()
        {
            var cancelledOnKey = "CancelledOn";
            var result = new ItemCancelledResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.Contains(cancelledOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtCancelledOnKey_EqualsCancelledOnDate()
        {
            var cancelledOn = DateTime.UtcNow;
            var cancelledOnKey = "CancelledOn";
            var result = new ItemCancelledResult(Guid.NewGuid(), cancelledOn);

            Assert.AreEqual(cancelledOn, result.Data[cancelledOnKey]);
        }
    }
}