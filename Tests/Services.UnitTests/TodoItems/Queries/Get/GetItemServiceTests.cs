using System;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Queries.GetItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Queries.Get
{
    [TestFixture]
    public class GetItemServiceTests
    {
        [Test]
        public void GetItem_WhenItemDoesNotExist_ThrowsNotFoundException()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var service = new GetItemService(mockRepository.Object, mockMapper.Object);

            mockRepository.Setup(a => a.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<NotFoundException>(() => service.GetItem(Guid.NewGuid()));
        }

        //[Test]
        //public async Task Handle_WhenItemExists_ReturnsItemDetails()
        //{
        //    var item = TodoItemFactory.GenerateItem();
        //    var childItems = new List<TodoItem>();
        //    var details = TodoItemFactory.MappedItemDetails(item);

        //    var mockRepository = new Mock<IContextRepository<ITodoContext>>();
        //    var mockMapper = new Mock<IMapper>();

        //    var query = new GetItemByIdRequest(item.ItemId);
        //    var service = new GetItemService(mockRepository.Object, mockMapper.Object);

        //    mockRepository.Setup(a => a.GetAsync(query.Specification)).ReturnsAsync(item);
        //    mockRepository.Setup(a => a.ListAsync(It.IsAny<GetItemsByParentId>())).ReturnsAsync(childItems);
        //    mockMapper.Setup(a => a.Map<TodoItemDetails>(item)).Returns(details);

        //    var result = await service.GetItem(Guid.NewGuid());

        //    Assert.AreEqual(details, result);
        //}
    }
}