using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemUpdatedResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidResult()
        {
            var result = new ItemUpdatedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemValidResult>(result);
        }

        [Test]
        public void Data_ContainsModifiedOnKey()
        {
            var modifiedOnKey = "ModifiedOn";
            var result = new ItemUpdatedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.Contains(modifiedOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtModifiedOnKey_EqualsModifiedOnDate()
        {
            var modifiedOn = DateTime.UtcNow;
            var modifiedOnKey = "ModifiedOn";
            var result = new ItemUpdatedResult(Guid.NewGuid(), modifiedOn);

            Assert.AreEqual(modifiedOn, result.Data[modifiedOnKey]);
        }
    }
}