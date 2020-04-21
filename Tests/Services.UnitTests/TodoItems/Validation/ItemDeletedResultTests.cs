using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemDeletedResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidResult()
        {
            var result = new ItemDeletedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemValidResult>(result);
        }

        [Test]
        public void Data_ContainsDeletedOnKey()
        {
            var deletedOnKey = "DeletedOn";
            var result = new ItemDeletedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.Contains(deletedOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtDeletedOnKey_EqualsDeletedOnDate()
        {
            var deletedOn = DateTime.UtcNow;
            var deletedOnKey = "DeletedOn";
            var result = new ItemDeletedResult(Guid.NewGuid(), deletedOn);

            Assert.AreEqual(deletedOn, result.Data[deletedOnKey]);
        }
    }
}