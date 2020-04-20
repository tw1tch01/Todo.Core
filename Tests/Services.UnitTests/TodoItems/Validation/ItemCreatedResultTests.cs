using System;
using System.Linq;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemCreatedResultTests
    {
        [Test]
        public void IsInstanceOf_ItemValidResult()
        {
            var result = new ItemCreatedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.IsInstanceOf<ItemValidResult>(result);
        }

        [Test]
        public void Data_ContainsCreatedOnKey()
        {
            var createdOnKey = "CreatedOn";
            var result = new ItemCreatedResult(Guid.NewGuid(), DateTime.UtcNow);

            Assert.Contains(createdOnKey, result.Data.Keys.ToList());
        }

        [Test]
        public void Data_AtCreatedOnKey_EqualsCreatedOnDate()
        {
            var createdOn = DateTime.UtcNow;
            var createdOnKey = "CreatedOn";
            var result = new ItemCreatedResult(Guid.NewGuid(), createdOn);

            Assert.AreEqual(createdOn, result.Data[createdOnKey]);
        }
    }
}