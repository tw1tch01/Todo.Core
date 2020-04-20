using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Commands.CancelItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoItems.Commands.CancelItem
{
    [TestFixture]
    public class CancelItemServiceTests
    {
        [Test]
        public async Task CancelItem_WhenNoItemFound_ReturnsItemNotFoundResult()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);
            var service = new CancelItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CancelItem(Guid.NewGuid());

            Assert.IsInstanceOf<ItemNotFoundResult>(result);
        }

        [Test]
        public async Task CancelItem_WhenItemIsCancelled_ReturnItemPreviouslyCancelledResult()
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

            var result = await service.CancelItem(item.ItemId);

            Assert.IsInstanceOf<ItemPreviouslyCancelledResult>(result);
        }

        [Test]
        public async Task CancelItem_WhenItemIsCompleted_ReturnsItemPreviouslyCompletedResult()
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

            var result = await service.CancelItem(item.ItemId);

            Assert.IsInstanceOf<ItemPreviouslyCompletedResult>(result);
        }

        [Test]
        public async Task CancelItem_WhenItemCanBeCancelled_ReturnsItemCancelledResult()
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

            var result = await service.CancelItem(item.ItemId);

            Assert.IsInstanceOf<ItemCancelledResult>(result);
        }

        [Test]
        public async Task CancelItem_WhenItemCanBeCancelled_SetsCancelledOn()
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

            var result = await service.CancelItem(item.ItemId);

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

            var result = await service.CancelItem(parentItem.ItemId);

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

            var result = await service.CancelItem(Guid.NewGuid());

            Assert.Multiple(() =>
            {
                mockItem.Verify(a => a.CancelItem(), Times.Once);
                mockRepository.Verify(a => a.SaveAsync(), Times.Once);
            });
        }
    }
}