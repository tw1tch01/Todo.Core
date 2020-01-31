using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using MediatR;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Commands.Actions.CompleteItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Commands.Actions
{
    [TestFixture]
    public class CompleteItemServiceTests
    {
        [Test]
        public void Handle_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMediator = new Mock<IMediator>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var service = new CompleteItemService(mockRepository.Object, mockMediator.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await service.CompleteItem(Guid.NewGuid()));
        }

        [Test]
        public void Handle_WhenItemIsCancelled_ThrowsItemPreviouslyCancelledException()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CancelledOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMediator = new Mock<IMediator>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new CompleteItemService(mockRepository.Object, mockMediator.Object);

            Assert.ThrowsAsync<ItemPreviouslyCancelledException>(async () => await service.CompleteItem(item.ItemId));
        }

        [Test]
        public void Handle_WhenItemIsCompleted_ThrowsItemPreviouslyCompletedException()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CompletedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMediator = new Mock<IMediator>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new CompleteItemService(mockRepository.Object, mockMediator.Object);

            Assert.ThrowsAsync<ItemPreviouslyCompletedException>(async () => await service.CompleteItem(item.ItemId));
        }

        [Test]
        public async Task Handle_WhenItemFound_SetsCompletedOn()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CompletedOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMediator = new Mock<IMediator>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new CompleteItemService(mockRepository.Object, mockMediator.Object);

            await service.CompleteItem(item.ItemId);

            Assert.IsNotNull(item.CompletedOn);
        }

        [Test]
        public async Task Handle_WhenItemWithChildExists_CompletesAllCompletableItems()
        {
            var parentItem = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CompletedOn = null
            };
            parentItem.ChildItems.Add(new TodoItem
            {
                CompletedOn = null
            });
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMediator = new Mock<IMediator>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == parentItem.ItemId))).ReturnsAsync(() => parentItem);

            var service = new CompleteItemService(mockRepository.Object, mockMediator.Object);

            await service.CompleteItem(parentItem.ItemId);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(parentItem.CompletedOn);
                Assert.That(parentItem.ChildItems.All(i => i.CompletedOn.HasValue));
            });
        }
    }
}