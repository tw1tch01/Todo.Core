using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Commands.Actions;
using Todo.Application.TodoItems.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;

namespace Todo.Application.UnitTests.TodoItems.Commands.Actions
{
    [TestFixture]
    public class CompleteItemRequestTests
    {
        [Test]
        public void Handle_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new CompleteItemRequest(Guid.NewGuid());
            var handler = new CompleteItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public void Handle_WhenItemIsCancelled_ThrowsItemPreviouslyCancelledException()
        {
            var item = new TodoItem
            {
                CancelledOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new CompleteItemRequest(Guid.NewGuid());
            var handler = new CompleteItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<ItemPreviouslyCancelledException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public void Handle_WhenItemIsCompleted_ThrowsItemPreviouslyCompletedException()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new CompleteItemRequest(Guid.NewGuid());
            var handler = new CompleteItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<ItemPreviouslyCompletedException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public async Task Handle_WhenItemFound_SetsCompletedOn()
        {
            var item = new TodoItem
            {
                CompletedOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new CompleteItemRequest(Guid.NewGuid());
            var handler = new CompleteItemRequest.RequestHandler(mockRepository.Object);

            await handler.Handle(command, default);

            Assert.IsNotNull(item.CompletedOn);
        }

        [Test]
        public async Task Handle_WhenItemWithChildExists_CompletesAllCompletableItems()
        {
            var parentItem = new TodoItem
            {
                CompletedOn = null
            };
            parentItem.ChildItems.Add(new TodoItem
            {
                CompletedOn = null
            });
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => parentItem);

            var command = new CompleteItemRequest(Guid.NewGuid());
            var handler = new CompleteItemRequest.RequestHandler(mockRepository.Object);

            await handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(parentItem.CompletedOn);
                Assert.That(parentItem.ChildItems.All(i => i.CompletedOn.HasValue));
            });
        }
    }
}