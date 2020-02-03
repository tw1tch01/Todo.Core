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
using Todo.Services.TodoItems.Commands.UpdateItem;
using Todo.Services.TodoItems.Specifications;

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

            Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateItem(Guid.NewGuid(), null));
        }

        [Test]
        public void UpdateItem_WhenParentItemDoesNotExist_ThrowsNotFoundException()
        {
            var itemDto = new UpdateItemDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => null);

            var service = new UpdateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            Assert.ThrowsAsync<NotFoundException>(() => service.UpdateItem(Guid.NewGuid(), itemDto));
        }
    }
}