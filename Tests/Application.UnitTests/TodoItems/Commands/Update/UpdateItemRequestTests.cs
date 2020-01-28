using System;
using Moq;
using NUnit.Framework;
using Todo.Application.TodoItems.Commands.Update;

namespace Todo.Application.UnitTests.TodoItems.Commands.Update
{
    [TestFixture]
    public class UpdateItemRequestTests
    {
        [Test]
        public void WhenItemDtoIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new UpdateItemRequest(It.IsAny<Guid>(), null));
        }
    }
}