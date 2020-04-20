using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Commands.UpdateItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoItems.Commands.UpdateItem
{
    [TestFixture]
    public class UpdateItemRequestTests
    {
        [Test]
        public void UpdateItem_WhenItemDtoIsNull_ThrowsArgumentNullException()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            var service = new UpdateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.UpdateItem(Guid.NewGuid(), null));
        }

        [Test]
        public async Task UpdateItem_WhenItemDoesNotExist_ReturnsItemNotFoundResult()
        {
            var itemDto = new UpdateItemDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => null);
            var service = new UpdateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.UpdateItem(Guid.NewGuid(), itemDto);

            Assert.IsInstanceOf<ItemNotFoundResult>(result);
        }

        [Test]
        public async Task UpdateItem_WhenItemExists_ReturnsItemUpdatedResult()
        {
            var itemDto = new UpdateItemDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => new TodoItem { ModifiedOn = DateTime.UtcNow });
            var service = new UpdateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.UpdateItem(Guid.NewGuid(), itemDto);

            Assert.IsInstanceOf<ItemUpdatedResult>(result);
        }
    }
}