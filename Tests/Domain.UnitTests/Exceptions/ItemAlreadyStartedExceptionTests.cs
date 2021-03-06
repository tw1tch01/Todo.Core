﻿using System;
using NUnit.Framework;
using Todo.Domain.Exceptions;

namespace Todo.Domain.UnitTests.Exceptions
{
    [TestFixture]
    public class ItemAlreadyStartedExceptionTests
    {
        [Test]
        public void WhenThrown_ReturnsCorrectMessage()
        {
            var startedOn = DateTime.Now;
            var itemId = Guid.NewGuid();
            var exception = Assert.Catch<ItemAlreadyStartedException>(() => throw new ItemAlreadyStartedException(startedOn, itemId));
            Assert.AreEqual($"Item already started on {startedOn}. (ItemId: {itemId})", exception.Message);
        }
    }
}