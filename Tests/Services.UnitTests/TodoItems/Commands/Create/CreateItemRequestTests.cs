using System;
using NUnit.Framework;
using Todo.Application.TodoItems.Commands.Create;

namespace Todo.Application.UnitTests.TodoItems.Commands.Create
{
    [TestFixture]
    public class CreateItemRequestTests
    {
        [Test]
        public void WhenItemDtoIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CreateItemRequest(null));
        }
    }
}