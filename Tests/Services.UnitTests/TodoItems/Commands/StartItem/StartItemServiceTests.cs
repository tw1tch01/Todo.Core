using System;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Commands.StartItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoItems.Commands.StartItem
{
    [TestFixture]
    public class StartItemServiceTests
    {
        [Test]
        public async Task StartItem_WhenNoItemFound_ReturnsItemNotFoundResult()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);
            var service = new StartItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.StartItem(Guid.NewGuid());

            Assert.IsInstanceOf<ItemNotFoundResult>(result);
        }

        [Test]
        public async Task StartItem_WhenItemAlreadyStarted_ReturnsItemAlreadyStartedResult()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                StartedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);
            var service = new StartItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.StartItem(item.ItemId);

            Assert.IsInstanceOf<ItemAlreadyStartedResult>(result);
        }

        [Test]
        public async Task StartItem_WhenItemAlreadyCancelled_ReturnsItemPreviouslyCancelledResult()
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
            var service = new StartItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.StartItem(item.ItemId);

            Assert.IsInstanceOf<ItemPreviouslyCancelledResult>(result);
        }

        [Test]
        public async Task StartItem_WhenItemAlreadyCompleted_ReturnsItemPreviouslyCompletedResult()
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
            var service = new StartItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.StartItem(item.ItemId);

            Assert.IsInstanceOf<ItemPreviouslyCompletedResult>(result);
        }

        [Test]
        public async Task StartItem_WhenCanBeStarted_ReturnsItemStartedResult()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                StartedOn = null,
                CancelledOn = null,
                CompletedOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new StartItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.StartItem(item.ItemId);

            Assert.IsInstanceOf<ItemStartedResult>(result);
        }

        [Test]
        public async Task StartItem_WhenCanBeStarted_StartsItem()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                StartedOn = null,
                CancelledOn = null,
                CompletedOn = null
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new StartItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            await service.StartItem(item.ItemId);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(item.StartedOn);
                Assert.IsNull(item.CancelledOn);
                Assert.IsNull(item.CompletedOn);
            });
        }

        [Test]
        public async Task StartItem_VerifyingSaveAsyncIsCalled()
        {
            var mockItem = new Mock<TodoItem>();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => mockItem.Object);
            mockItem.Object.StartedOn = DateTime.UtcNow;

            var service = new StartItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            await service.StartItem(Guid.NewGuid());

            Assert.Multiple(() =>
            {
                mockItem.Verify(a => a.StartItem(), Times.Once);
                mockRepository.Verify(a => a.SaveAsync(), Times.Once);
            });
        }
    }
}