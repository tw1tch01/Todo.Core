using System;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Commands.CreateItem
{
    [TestFixture]
    public class CreateItemServiceTests
    {
        [Test]
        public void AddChildItem_WhenChildItemDtoIsNull_ThrowsArgumentNullException()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.AddChildItem(Guid.NewGuid(), null));
        }

        [Test]
        public void AddChildItem_WhenParentItemDoesNotExist_ThrowsNotFoundException()
        {
            var itemDto = new CreateItemDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => null);

            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await service.AddChildItem(Guid.NewGuid(), itemDto));
        }

        [Test]
        public void CreateItem_WhenItemIsNull_ThrowsArgumentNullException()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.CreateItem(null));
        }
    }
}