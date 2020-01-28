using System;
using System.Linq;
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
    public class CancelItemRequestTests
    {
        [Test]
        public void Handle_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new CancelItemRequest(Guid.NewGuid());
            var handler = new CancelItemRequest.RequestHandler(mockRepository.Object);

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

            var command = new CancelItemRequest(Guid.NewGuid());
            var handler = new CancelItemRequest.RequestHandler(mockRepository.Object);

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

            var command = new CancelItemRequest(Guid.NewGuid());
            var handler = new CancelItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<ItemPreviouslyCompletedException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public async Task Handle_WhenItemFound_SetsCancelledOn()
        {
            var item = new TodoItem
            {
                CancelledOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new CancelItemRequest(Guid.NewGuid());
            var handler = new CancelItemRequest.RequestHandler(mockRepository.Object);

            await handler.Handle(command, default);

            Assert.IsNotNull(item.CancelledOn);
        }

        [Test]
        public async Task Handle_WhenItemWithChildExists_CancelsAllCancellableItems()
        {
            var parentItem = new TodoItem
            {
                CancelledOn = null
            };
            parentItem.ChildItems.Add(new TodoItem
            {
                CancelledOn = null
            });
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => parentItem);

            var command = new CancelItemRequest(Guid.NewGuid());
            var handler = new CancelItemRequest.RequestHandler(mockRepository.Object);

            await handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(parentItem.CancelledOn);
                Assert.That(parentItem.ChildItems.All(i => i.CancelledOn.HasValue));
            });
        }
    }
}