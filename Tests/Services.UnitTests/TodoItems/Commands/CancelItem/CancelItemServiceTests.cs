using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.Notifications;
using Todo.Services.Workflows;
using Todo.Services.TodoItems.Commands.CancelItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Commands.CancelItem
{
    [TestFixture]
    public class CancelItemServiceTests
    {
        [Test]
        public void CancelItem_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var service = new CancelItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await service.CancelItem(Guid.NewGuid()));
        }

        [Test]
        public void CancelItem_WhenItemIsCancelled_ThrowsItemPreviouslyCancelledException()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CancelledOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new CancelItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ItemPreviouslyCancelledException>(async () => await service.CancelItem(item.ItemId));
        }

        [Test]
        public void CancelItem_WhenItemIsCompleted_ThrowsItemPreviouslyCompletedException()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CompletedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new CancelItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ItemPreviouslyCompletedException>(async () => await service.CancelItem(item.ItemId));
        }

        [Test]
        public async Task CancelItem_WhenItemFound_SetsCancelledOn()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CancelledOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new CancelItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            await service.CancelItem(item.ItemId);

            Assert.IsNotNull(item.CancelledOn);
        }

        [Test]
        public async Task CancelItem_WhenItemWithChildExists_CancelsAllCancellableItems()
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
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == parentItem.ItemId))).ReturnsAsync(() => parentItem);

            var service = new CancelItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            await service.CancelItem(parentItem.ItemId);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(parentItem.CancelledOn);
                Assert.That(parentItem.ChildItems.All(i => i.CancelledOn.HasValue));
            });
        }

        [Test]
        public async Task CancelItem_VerifyingSaveAsyncIsCalled()
        {
            var mockItem = new Mock<TodoItem>();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => mockItem.Object);
            mockItem.Object.CancelledOn = DateTime.UtcNow;

            var service = new CancelItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            await service.CancelItem(Guid.NewGuid());

            Assert.Multiple(() =>
            {
                mockItem.Verify(a => a.CancelItem(), Times.Once);
                mockRepository.Verify(a => a.SaveAsync(), Times.Once);
            });
        }
    }
}