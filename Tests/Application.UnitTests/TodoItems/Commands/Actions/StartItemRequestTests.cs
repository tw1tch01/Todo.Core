using System;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Commands.Actions;
using Todo.Application.TodoItems.Queries.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;

namespace Todo.Application.UnitTests.TodoItems.Commands.Actions
{
    [TestFixture]
    public class StartItemRequestTests
    {
        [Test]
        public void Handle_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new StartItemRequest(Guid.NewGuid());
            var handler = new StartItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public void Handle_WhenItemAlreadyStarted_ThrowsItemAlreadyStartedException()
        {
            var item = new TodoItem
            {
                StartedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new StartItemRequest(Guid.NewGuid());
            var handler = new StartItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<ItemAlreadyStartedException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public void Handle_WhenItemAlreadyCancelled_ThrowsItemAlreadyCancelledException()
        {
            var item = new TodoItem
            {
                CancelledOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new StartItemRequest(Guid.NewGuid());
            var handler = new StartItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<ItemPreviouslyCancelledException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public void Handle_WhenItemAlreadyCompleted_ThrowsItemAlreadyCompletedException()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new StartItemRequest(Guid.NewGuid());
            var handler = new StartItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<ItemPreviouslyCompletedException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public async Task Handle_WhenItemExists_ResetsProperties()
        {
            var item = new TodoItem
            {
                StartedOn = null,
                CancelledOn = null,
                CompletedOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new StartItemRequest(Guid.NewGuid());
            var handler = new StartItemRequest.RequestHandler(mockRepository.Object);

            await handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(item.StartedOn);
                Assert.IsNull(item.CancelledOn);
                Assert.IsNull(item.CompletedOn);
            });
        }
    }
}