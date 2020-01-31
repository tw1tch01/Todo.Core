using System;
using Data.Repositories;
using MediatR;
using Moq;
using NUnit.Framework;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Commands.DeleteItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Commands.DeleteItem
{
    [TestFixture]
    public class DeleteItemServiceTests
    {
        [Test]
        public void AddChildItem_WhenParentItemDoesNotExist_ThrowsNotFoundException()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMediator = new Mock<IMediator>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => null);

            var service = new DeleteItemService(mockRepository.Object, mockMediator.Object);

            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteItem(Guid.NewGuid()));
        }
    }
}