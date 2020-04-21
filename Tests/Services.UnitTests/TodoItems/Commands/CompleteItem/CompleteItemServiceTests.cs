using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Commands.CompleteItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoItems.Commands.CompleteItem
{
    [TestFixture]
    public class CompleteItemServiceTests
    {
        [Test]
        public async Task CompleteItem_WhenNoItemFound_ReturnsItemNotFoundResult()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);
            var service = new CompleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CompleteItem(Guid.NewGuid());

            Assert.IsInstanceOf<ItemNotFoundResult>(result);
        }

        [Test]
        public async Task CompleteItem_WhenItemIsCancelled_ReturnsItemPreviouslyCancelledResult()
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
            var service = new CompleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CompleteItem(item.ItemId);

            Assert.IsInstanceOf<ItemPreviouslyCancelledResult>(result);
        }

        [Test]
        public async Task CompleteItem_WhenItemIsCompleted_ReturnsItemPreviouslyCompletedResult()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CompletedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);
            var service = new CompleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CompleteItem(item.ItemId);

            Assert.IsInstanceOf<ItemPreviouslyCompletedResult>(result);
        }

        [Test]
        public async Task CompleteItem_WhenItemCanBeCompleted_ReturnsItemCompletedResult()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CompletedOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);
            var service = new CompleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CompleteItem(item.ItemId);

            Assert.IsInstanceOf<ItemCompletedResult>(result);
        }

        [Test]
        public async Task CompleteItem_WhenItemCanBeCompleted_SetsCompletedOn()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CompletedOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);
            var service = new CompleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CompleteItem(item.ItemId);

            Assert.IsNotNull(item.CompletedOn);
        }

        [Test]
        public async Task CompleteItem_WhenItemWithChildExists_CompletesAllCompletableItems()
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
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == parentItem.ItemId))).ReturnsAsync(() => parentItem);

            var service = new CompleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CompleteItem(parentItem.ItemId);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(parentItem.CompletedOn);
                Assert.That(parentItem.ChildItems.All(i => i.CompletedOn.HasValue));
            });
        }

        [Test]
        public async Task CompleteItem_VerifyingSaveAsyncIsCalled()
        {
            var mockItem = new Mock<TodoItem>();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => mockItem.Object);
            mockItem.Object.CompletedOn = DateTime.UtcNow;

            var service = new CompleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CompleteItem(Guid.NewGuid());

            Assert.Multiple(() =>
            {
                mockItem.Verify(a => a.CompleteItem(), Times.Once);
                mockRepository.Verify(a => a.SaveAsync(), Times.Once);
            });
        }
    }
}