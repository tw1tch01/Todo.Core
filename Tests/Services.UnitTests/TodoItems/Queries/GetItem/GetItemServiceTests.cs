using System;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Queries.GetItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Queries.GetItem
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
    }
}