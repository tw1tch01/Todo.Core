using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.Notifications;
using Todo.Services.Workflows;
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
        public async Task AddChildItem_VerifyingSaveAsyncIsCalled()
        {
            var mockCreateDto = new Mock<CreateItemDto>();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockMapper.Setup(m => m.Map<TodoItem>(mockCreateDto.Object)).Returns(new TodoItem
            {
                ItemId = Guid.NewGuid()
            });
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => new TodoItem
            {
                ItemId = Guid.NewGuid()
            });

            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            await service.AddChildItem(Guid.NewGuid(), mockCreateDto.Object);

            mockRepository.Verify(a => a.SaveAsync(), Times.Once);
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

        [Test]
        public async Task CreateItem_VerifyingSaveAsyncIsCalled()
        {
            var mockCreateDto = new Mock<CreateItemDto>();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockMapper.Setup(m => m.Map<TodoItem>(mockCreateDto.Object)).Returns(new TodoItem
            {
                ItemId = Guid.NewGuid()
            });

            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            await service.CreateItem(mockCreateDto.Object);

            mockRepository.Verify(a => a.SaveAsync(), Times.Once);
        }
    }
}