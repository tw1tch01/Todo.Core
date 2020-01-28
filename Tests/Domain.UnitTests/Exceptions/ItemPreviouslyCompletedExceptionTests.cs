using System;
using NUnit.Framework;
using Todo.Domain.Exceptions;

namespace Todo.Domain.UnitTests.Exceptions
{
    [TestFixture]
    public class ItemPreviouslyCompletedExceptionTests
    {
        [Test]
        public void WhenThrown_ReturnsCorrectMessage()
        {
            var startedOn = DateTime.Now;
            var itemId = Guid.NewGuid();
            var exception = Assert.Catch<ItemPreviouslyCompletedException>(() => throw new ItemPreviouslyCompletedException(startedOn, itemId));
            Assert.AreEqual($"Item was previously completed on {startedOn}. (ItemId: {itemId})", exception.Message);
        }
    }
}