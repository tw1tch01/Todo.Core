using System;
using NUnit.Framework;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.UnitTests.TodoItems.Validation
{
    [TestFixture]
    public class ItemNotFoundResultTests
    {
        [Test]
        public void IsInstanceOf_ItemInvalidResult()
        {
            var result = new ItemNotFoundResult(Guid.NewGuid());

            Assert.IsInstanceOf<ItemInvalidResult>(result);
        }
    }
}