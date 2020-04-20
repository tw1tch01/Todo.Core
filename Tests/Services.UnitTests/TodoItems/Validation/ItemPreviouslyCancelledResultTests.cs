using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemPreviouslyCancelledResultTests
    {
        [Test]
        public void IsInstanceOf_ItemInvalidResult()
        {
            var result = new ItemPreviouslyCancelledResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemInvalidResult>(result);
        }

        [Test]
        public void Data_ContainsCancelledOnKey()
        {
            var cancelledOn = DateTime.UtcNow;
            var cancelledOnKey = "CancelledOn";
            var result = new ItemPreviouslyCancelledResult(Guid.NewGuid(), cancelledOn);

            Assert.Contains(cancelledOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtCancelledOnKey_EqualsCancelledOnDate()
        {
            var cancelledOn = DateTime.UtcNow;
            var cancelledOnKey = "CancelledOn";
            var result = new ItemPreviouslyCancelledResult(Guid.NewGuid(), cancelledOn);

            Assert.AreEqual(cancelledOn, result.Data[cancelledOnKey]);
        }
    }
}