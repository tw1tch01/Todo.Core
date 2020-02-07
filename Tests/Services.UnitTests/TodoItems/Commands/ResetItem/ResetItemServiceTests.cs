using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.Notifications;
using Todo.Services.Workflows;
using Todo.Services.TodoItems.Commands.ResetItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Commands.ResetItem
{
    [TestFixture]
    public class ResetItemServiceTests
    {
        [Test]
        public void ResetItem_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var service = new ResetItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await service.ResetItem(Guid.NewGuid()));
        }

        [Test]
        public async Task ResetItem_WhenItemExists_ResetsProperties()
        {
            var item = new TodoItem
            {
                StartedOn = DateTime.UtcNow,
                CancelledOn = DateTime.UtcNow,
                CompletedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new ResetItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            await service.ResetItem(item.ItemId);

            Assert.Multiple(() =>
            {
                Assert.IsNull(item.StartedOn);
                Assert.IsNull(item.CancelledOn);
                Assert.IsNull(item.CompletedOn);
            });
        }

        [Test]
        public async Task ResetItem_WhenItemWithChildExists_ResetsAllProperties()
        {
            var parentItem = new TodoItem
            {
                StartedOn = DateTime.UtcNow,
                CancelledOn = DateTime.UtcNow,
                CompletedOn = DateTime.UtcNow
            };
            parentItem.ChildItems.Add(new TodoItem
            {
                StartedOn = DateTime.UtcNow,
                CancelledOn = DateTime.UtcNow,
                CompletedOn = DateTime.UtcNow
            });
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == parentItem.ItemId))).ReturnsAsync(() => parentItem);

            var service = new ResetItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            await service.ResetItem(parentItem.ItemId);

            Assert.Multiple(() =>
            {
                Assert.IsNull(parentItem.StartedOn);
                Assert.IsNull(parentItem.CancelledOn);
                Assert.IsNull(parentItem.CompletedOn);
                Assert.That(parentItem.ChildItems.All(i => i.StartedOn == null));
                Assert.That(parentItem.ChildItems.All(i => i.CancelledOn == null));
                Assert.That(parentItem.ChildItems.All(i => i.CompletedOn == null));
            });
        }

        [Test]
        public async Task ResetItem_VerifyingSaveAsyncIsCalled()
        {
            var mockItem = new Mock<TodoItem>();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => mockItem.Object);

            var service = new ResetItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            await service.ResetItem(Guid.NewGuid());

            Assert.Multiple(() =>
            {
                mockItem.Verify(a => a.ResetItem(), Times.Once);
                mockRepository.Verify(a => a.SaveAsync(), Times.Once);
            });
        }
    }
}