using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.Services.Common;
using Todo.Services.Notifications;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Specifications;
using Todo.Services.TodoItems.Validation;
using Todo.Services.Workflows;

namespace Todo.Services.UnitTests.TodoItems.Commands.CreateItem
{
    [TestFixture]
    public class CreateItemServiceTests
    {
        private readonly IFixture _fixture = new Fixture();

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
        public async Task AddChildItem_WhenCreateItemDtoIsInvalid_ReturnsInvalidDtoResult()
        {
            var itemDto = new CreateItemDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.AddChildItem(Guid.NewGuid(), itemDto);

            Assert.IsInstanceOf(typeof(InvalidDtoResult), result);
        }

        [Test]
        public async Task AddChildItem_WhenParentItemDoesNotExist_ReturnsItemNotFoundResult()
        {
            var itemDto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => null);

            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.AddChildItem(Guid.NewGuid(), itemDto);

            Assert.IsInstanceOf(typeof(ItemNotFoundResult), result);
        }

        [Test]
        public async Task AddChildItem_WhenItemIsCreated_ReturnsItemCreatedResult()
        {
            var parentItem = new TodoItem
            {
                ItemId = Guid.NewGuid(),
            };
            var itemDto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => parentItem);
            mockMapper.Setup(m => m.Map<TodoItem>(itemDto)).Returns(new TodoItem { CreatedOn = DateTime.UtcNow });
            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.AddChildItem(parentItem.ItemId, itemDto);

            Assert.IsInstanceOf<ItemCreatedResult>(result);
        }

        [Test]
        public async Task AddChildItem_VerifyingSaveAsyncIsCalled()
        {
            var itemDto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();

            mockMapper.Setup(m => m.Map<TodoItem>(itemDto)).Returns(new TodoItem
            {
                ItemId = Guid.NewGuid(),
                Name = itemDto.Name,
                Description = itemDto.Description,
                CreatedOn = DateTime.UtcNow
            });
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => new TodoItem
            {
                ItemId = Guid.NewGuid()
            });

            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            await service.AddChildItem(Guid.NewGuid(), itemDto);

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
        public async Task CreateItem_WhenCreateItemDtoIsInvalid_ReturnsInvalidDtoResult()
        {
            var itemDto = new CreateItemDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CreateItem(itemDto);

            Assert.IsInstanceOf<InvalidDtoResult>(result);
        }

        [Test]
        public async Task CreateItem_WhenItemIsCreated_ReturnsItemCreatedResult()
        {
            var itemDto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockMapper.Setup(m => m.Map<TodoItem>(itemDto)).Returns(new TodoItem
            {
                ItemId = Guid.NewGuid(),
                Name = itemDto.Name,
                Description = itemDto.Description,
                CreatedOn = DateTime.UtcNow
            });
            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            var result = await service.CreateItem(itemDto);

            Assert.IsInstanceOf<ItemCreatedResult>(result);
        }

        [Test]
        public async Task CreateItem_VerifyingSaveAsyncIsCalled()
        {
            var itemDto = new CreateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>()
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockNotification = new Mock<INotificationService>();
            var mockWorkflow = new Mock<IWorkflowService>();
            mockMapper.Setup(m => m.Map<TodoItem>(itemDto)).Returns(new TodoItem
            {
                ItemId = Guid.NewGuid(),
                Name = itemDto.Name,
                Description = itemDto.Description,
                CreatedOn = DateTime.UtcNow
            });
            var service = new CreateItemService(mockRepository.Object, mockMapper.Object, mockNotification.Object, mockWorkflow.Object);

            await service.CreateItem(itemDto);

            mockRepository.Verify(a => a.SaveAsync(), Times.Once);
        }
    }
}