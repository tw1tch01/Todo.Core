using System;
using NUnit.Framework;
using Todo.Domain.Exceptions;

namespace Todo.Domain.UnitTests.Exceptions
{
    [TestFixture]
    public class ItemPreviouslyCancelledExceptionTests
    {
        [Test]
        public void WhenThrown_ReturnsCorrectMessage()
        {
            var startedOn = DateTime.Now;
            var itemId = Guid.NewGuid();
            var exception = Assert.Catch<ItemPreviouslyCancelledException>(() => throw new ItemPreviouslyCancelledException(startedOn, itemId));
            Assert.AreEqual($"Item was previously cancelled on {startedOn}. (ItemId: {itemId})", exception.Message);
        }
    }
}