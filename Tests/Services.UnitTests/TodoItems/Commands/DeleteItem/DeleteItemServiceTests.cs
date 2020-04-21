using System;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Commands.DeleteItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoItems.Commands.DeleteItem
{
    [TestFixture]
    public class DeleteItemServiceTests
    {
        [Test]
        public async Task DeleteItem_WhenItemDoesNotExist_ReturnsItemNotFoundResult()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => null);

            var service = new DeleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.DeleteItem(Guid.NewGuid());

            Assert.IsInstanceOf<ItemNotFoundResult>(result);
        }

        [Test]
        public async Task DeleteItem_WhenItemDoesExist_ReturnsItemDeletedResult()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => new TodoItem());

            var service = new DeleteItemService(mockRepository.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.DeleteItem(Guid.NewGuid());

            Assert.IsInstanceOf<ItemDeletedResult>(result);
        }
    }
}