using System;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoItems.Commands.StartItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Commands.Actions
{
    [TestFixture]
    public class StartItemServiceTests
    {
        [Test]
        public void Handle_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var service = new StartItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await service.StartItem(Guid.NewGuid()));
        }

        [Test]
        public void Handle_WhenItemAlreadyStarted_ThrowsItemAlreadyStartedException()
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

            Assert.ThrowsAsync<ItemAlreadyStartedException>(async () => await service.StartItem(item.ItemId));
        }

        [Test]
        public void Handle_WhenItemAlreadyCancelled_ThrowsItemAlreadyCancelledException()
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

            Assert.ThrowsAsync<ItemPreviouslyCancelledException>(async () => await service.StartItem(item.ItemId));
        }

        [Test]
        public void Handle_WhenItemAlreadyCompleted_ThrowsItemAlreadyCompletedException()
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

            Assert.ThrowsAsync<ItemPreviouslyCompletedException>(async () => await service.StartItem(item.ItemId));
        }

        [Test]
        public async Task Handle_WhenItemExists_ResetsProperties()
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
    }
}